using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SequencerAudio_v1 : NetworkBehaviour {

    #region Variables

    private static int currentRow = 0; // Used in PlayMusic() to keep track of current button row being played

    public AudioSource[] srcAudio; //array of audio sources that will be played when chosen

    GameObject sequencerState; // keeps track of sequencer state

    private bool serverfound = false; // Used to tell is the network server is active. Otherwise will get null object references

    #endregion Variables

    #region Methods

    private void Start()
    {
        InvokeRepeating("PlayMusic", 0.0f, 0.25f);
    }


    //Plays music
    public void PlayMusic()
    {
        if (!NetworkServer.active) //Checks if server is active
        {
            Debug.Log("Network Server is NOT active");
            return;
        }
        if(NetworkServer.active && !serverfound) // if server is active
        {
            Debug.Log("Network Server IS active");
            sequencerState = GameObject.FindGameObjectWithTag("SequencerState"); //Get the sequencer state object
            serverfound = true; // one-time use to make sure that this condition statement is never accessed again
        }

        //print("Executed: " + Time.time);
        var sequencerInfo = sequencerState.GetComponent<SequencerStateStatus>(); // Get SequencerStatusComponent

        int i = currentRow;
        print("sequencer onoff: " + sequencerInfo.bitsOnOrOff);
        //print("On/Off value: " + (sequencerInfo.onOff >> (i * 8)));
        ulong toTurn = sequencerInfo.bitsOnOrOff >> (i * 8); // Check 8-bits each iteration

        for (int j = 0; j < 8; j++)
        {
            //Debug.Log("To turn: " + ((toTurn >> j & 1) == 1));
            if ((toTurn >> j & 1) == 1)
                srcAudio[j].Play();
            

            //Change color of rows, row-by-row
            Image buttonImg = CreateSequencerButtons.buttons[i, j].GetComponent<Image>();
            if (buttonImg.color.Equals(Color.white) && !buttonImg.color.Equals(Color.blue))
                buttonImg.color = Color.yellow;
            else if (!buttonImg.color.Equals(Color.blue))
                buttonImg.color = Color.white;

        }
        currentRow++; //increment to next row
        if (currentRow > 7) currentRow = 0; //if at last row, set to 0
    }
    #endregion Methods
}
