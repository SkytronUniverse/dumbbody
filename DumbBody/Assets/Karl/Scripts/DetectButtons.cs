using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

/*
 * Detects button pressed by getting the information from CreateSequencerButtons.cs GetButtonName()
 * 
 * Attached to Player prefab
 */ 
public class DetectButtons : NetworkBehaviour {

    public static bool clicked = false; //keeps track of clicks (also accessed by CreateSequencerButtons.cs

    void Update()
    {
        Debug.Log("clicked = " + clicked);
        if (clicked) 
            CmdSendButtonData(); //Call command
    }


    // Command sent to SequencerStateObject
    [Command]
    public void CmdSendButtonData()
    {
        ulong bitToFlip = 0x00000000;

        var sequencerState = GameObject.FindGameObjectWithTag("SequencerState").GetComponent<SequencerStateStatus>();
        Debug.Log("Button Name in CMD: " + CreateSequencerButtons.buttonName);
        int shiftPos = int.Parse(CreateSequencerButtons.buttonName); // buttonName is an integer between 0 and 63
        bitToFlip ^= 1ul << shiftPos; // flips bits at the position of the button
        
        sequencerState.UpdateBits(bitToFlip); //call Update bits function in SequencerStateStatus.cs
        clicked = false; //set clicked back to false
    }
}
