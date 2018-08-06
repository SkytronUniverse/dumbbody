﻿using System.Collections;
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

    SequencerStateStatus sequencerStateManager;
    CreateSequencerButtons sequencer;

    public override void OnStartServer()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("SequencerState");
        if (temp != null)
        {
            sequencerStateManager = temp.GetComponent<SequencerStateStatus>();
        }

        GameObject temp_0 = GameObject.FindGameObjectWithTag("SequencerCanvas");
        if (temp != null)
        {
            sequencer = temp.GetComponent<CreateSequencerButtons>();
        }
    }

    public override void OnStartLocalPlayer()
    {
        while (sequencerStateManager == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("SequencerState");
            if (temp != null)
                sequencerStateManager = temp.GetComponent<SequencerStateStatus>();
        }

        while (sequencer == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("SequencerCanvas");
            if (temp != null)
                sequencer = temp.GetComponent<CreateSequencerButtons>();
        }
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        Debug.Log("clicked = " + clicked);
        if (clicked)
            CmdSendButtonData(); //Call command
            
    }


    // Command sent to SequencerStateObject
    [Command]
    public void CmdSendButtonData()
    {
       ulong bitToFlip = 0x00000000;

        //var playerController = transform.GetComponent<PlayerController>();

        //var sequencerState = GameObject.FindGameObjectWithTag("SequencerState").GetComponent<SequencerStateStatus>();
        //var sequencer = GameObject.FindGameObjectWithTag("SequencerCanvas").GetComponent<CreateSequencerButtons>();
        Debug.Log("Button Name in CMD: " + sequencer.buttonNumber);
        int shiftPos = sequencer.buttonNumber; // buttonNumber is an integer between 0 and 63
        bitToFlip ^= 1ul << shiftPos; // flips bits at the position of the button

        sequencerStateManager.UpdateBits(bitToFlip); //call Update bits function in SequencerStateStatus.cs
        //playerController.bitsFlipped = sequencerState.bitsOnOrOff;
        //playerController.bitsFlipped = sequencerState.bitsOnOrOff;
        //PlayerController.bitsChanged = true;

        //sequencerState.UpdateList(sequencer.buttonNumber);
        clicked = false; //set clicked back to false
    }
}
