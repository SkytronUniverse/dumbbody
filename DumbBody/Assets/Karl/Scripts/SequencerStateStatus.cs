using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * This is meant to keep track of the buttons pressed on sequencers across the network.
 * 
 * Attached to SequencerStateObject prefab that is a Registered Spawnable prefab with the NetworkManager. It has a NetworkIdentity with none of the boxes
 * checked (local player authority or server authority)
 */ 
public class SequencerStateStatus : NetworkBehaviour {

    [SyncVar]
    public ulong bitsOnOrOff = 0x00000000; //ulong to use for bit manipulation

    SyncListBool buttons = new SyncListBool();

    public override void OnStartServer()
    {
        for (int i = 0; i < 64; i++)
            buttons.Add(false);
    }

    // Update the SyncVar bitsOnOff so that hopefully all the clients will have the exact same variable to access
    public void UpdateBits(ulong bits)
    {
        if (!isServer) // if not server, return
            return;

        Debug.Log("Flipping bits...");
        bitsOnOrOff ^= bits; //flip bits
        Debug.Log("Sequencer State bits: " + bitsOnOrOff);
        
        RpcUpdateSequencerButtons(bitsOnOrOff); // call RpcChangeBits() function in PlayerController to change button color for each client

    }

    public void UpdateList(int buttonNumber)
    {
        if (!isServer) // if not server, return
            return;

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // find the player object

        if (!buttons[buttonNumber])
        {
            buttons[buttonNumber] = true;
        } else
        {
            buttons[buttonNumber] = false;
        }

        //player.RpcManageSequencer(buttonNumber); // call RpcChangeBits() function in PlayerController to change button color for each client
        
    }
    [ClientRpc]
    public void RpcUpdateSequencerButtons(ulong bits)
    {
        if (isLocalPlayer)
        {
            var sequencerButtons = GameObject.FindGameObjectWithTag("SequencerCanvas").GetComponent<CreateSequencerButtons>();
            for (int i = 0; i < 64; i++)
            {
                Image buttonImg = sequencerButtons.buttons[i / 8, i % 8].GetComponent<Image>();
                if ((bitsOnOrOff & 1) == 1)
                    buttonImg.color = Color.blue;
                else
                    buttonImg.color = Color.white;
            }
        }
}

}
