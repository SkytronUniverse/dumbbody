using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/*
 * This is meant to keep track of the buttons pressed on sequencers across the network.
 * 
 * Attached to SequencerStateObject that is in the scene. It has a NetworkIdentity with none of the boxes
 * checked (local player authority or server authority)
 */
public class SequencerStateStatus : NetworkBehaviour {

    #region Variables

    [SyncVar]
    public ulong bitsOnOrOff = 0x00000000; //ulong to use for bit manipulation

    #endregion Variables


    #region Methods
    // Update the SyncVar bitsOnOff so that hopefully all the clients will have the exact same variable to access

    public void UpdateBits(ulong bits)
    {
        // if not server, return
        if (!isServer)
        {
            Debug.Log("Not server... returning");
            return;
        }

        Debug.Log("Flipping bits...");
        bitsOnOrOff ^= bits; //flip bits
        Debug.Log("Sequencer State bits: " + bitsOnOrOff);

    }

    #endregion Methods
}
