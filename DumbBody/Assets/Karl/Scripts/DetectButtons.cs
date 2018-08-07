using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

/*
 * Detects button pressed by getting the information from CreateSequencerButtons.instance.cs GetButtonName()
 * 
 * Attached to Player prefab
 */ 
public class DetectButtons : NetworkBehaviour {

    SequencerStateStatus sequencerStateManager;

    private ulong bitToFlip = 0x00000000;

    [SyncVar]
    private bool isClicked = false;

    void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("SequencerState");
        if (temp != null)
        {
            sequencerStateManager = temp.GetComponent<SequencerStateStatus>();
        }

       
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    public override void OnStartLocalPlayer()
    {
        while (sequencerStateManager == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("SequencerState");
            if (temp != null)
                sequencerStateManager = temp.GetComponent<SequencerStateStatus>();
        }

        
        bitToFlip = sequencerStateManager.bitsOnOrOff;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            Debug.Log("Not Local Player");
            return;
        }

        isClicked = CreateSequencerButtons.instance.clicked;

        Debug.Log("clicked = " + isClicked);
        if (isClicked)
        {
            Debug.Log("Button Name in CMD: " + CreateSequencerButtons.instance.buttonNumber);
            int shiftPos = CreateSequencerButtons.instance.buttonNumber; // buttonNumber is an integer between 0 and 63
            bitToFlip ^= 1ul << shiftPos;
            Debug.Log(bitToFlip);
            CreateSequencerButtons.instance.clicked = false;
            CmdSendButtonData(bitToFlip); //Call command
            bitToFlip = 0x00000000;
        }
            
    }


    // Command sent to SequencerStateObject
    [Command]
    public void CmdSendButtonData(ulong bits)
    {
        Debug.Log("Clicked = " + CreateSequencerButtons.instance.clicked);
       //ulong bitToFlip = 0x00000000;

        //var playerController = transform.GetComponent<PlayerController>();

        //var sequencerState = GameObject.FindGameObjectWithTag("SequencerState").GetComponent<SequencerStateStatus>();
        //var sequencer = GameObject.FindGameObjectWithTag("SequencerCanvas").GetComponent<CreateSequencerButtons.instance>();
       // Debug.Log("Button Name in CMD: " + sequencer.buttonNumber);
        //int shiftPos = sequencer.buttonNumber; // buttonNumber is an integer between 0 and 63
        //bitToFlip ^= 1ul << shiftPos; // flips bits at the position of the button

        sequencerStateManager.UpdateBits(bits); //call Update bits function in SequencerStateStatus.cs
        //playerController.bitsFlipped = sequencerState.bitsOnOrOff;
        //playerController.bitsFlipped = sequencerState.bitsOnOrOff;
        //PlayerController.bitsChanged = true;

        //sequencerState.UpdateList(sequencer.buttonNumber);
        isClicked = false; //set clicked back to false
    }
}
