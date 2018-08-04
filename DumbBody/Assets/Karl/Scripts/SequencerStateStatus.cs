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


    // Update the SyncVar bitsOnOff so that hopefully all the clients will have the exact same variable to access
    public void UpdateBits(ulong bits)
    {
        if (!isServer) // if not server, return
            return;

        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // find the player object
        Debug.Log("Flipping bits...");
        bitsOnOrOff ^= bits; //flip bits
        Debug.Log(bitsOnOrOff);
        player.RpcChangeBits(bitsOnOrOff); // call RpcChangeBits() function in PlayerController to change button color for each client
        
    }

}
