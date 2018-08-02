using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerAudio_v1 : MonoBehaviour {

    #region Variables
    private readonly int row = CreateSequencerButtons.sizeI;
    private readonly int col = CreateSequencerButtons.sizeJ;

    private static int currentRow = 0;
    private static int currentButton = 0;

    public bool[,] audio_to_play;

    public AudioSource[] srcAudio;

    float tempTime = 0.0f;

    #endregion Variables

    #region Methods

    private void Start()
    {
        print(row);
        print(col);

        audio_to_play = new bool[8, 16];

        for (int i =0; i < 8; i++)
        {
            for(int j =0; j < 16; j++)
            {
                audio_to_play[i, j] = false;
            }
        }

        InvokeRepeating("PlayMusic", 0.0f, 0.25f);

    }
    

    public void SetAudioToPlay(string info)
    {
        int i = int.Parse(info.Substring(0, 1));
        int j = int.Parse(info.Substring(1, 1), System.Globalization.NumberStyles.HexNumber);
        string buttonName = info.Substring(2);

        GameObject b = GameObject.Find(buttonName);
        if (!audio_to_play[i, j])
        {
            audio_to_play[i, j] = true;
            b.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            audio_to_play[i, j] = false;
            b.GetComponent<Image>().color = Color.white;
        }

    }


    public void PlayMusic()
    {
        //print("Executed: " + Time.time);
        CreateSequencerButtons.instance.OnSetButton(CreateSequencerButtons.instance.onOff);
        int i = currentRow;
        print("On/Off value: " + (CreateSequencerButtons.instance.onOff >> (i * 8)));
        ulong toTurn = CreateSequencerButtons.instance.onOff >> (i * 8);
        print(i);

        for (int j = 0; j < 8; j++)
        {
            Debug.Log("To turn: " + ((toTurn >> j & 1) == 1));
            if ((toTurn >> j & 1) == 1)
                srcAudio[j].Play();
            Image buttonImg = CreateSequencerButtons.instance.buttons[i, j].GetComponent<Image>();
            print("Button name: " + CreateSequencerButtons.instance.buttons[i, j].name + " Color: " + buttonImg.color.ToString());
            if (buttonImg.color.Equals(Color.white) && !buttonImg.color.Equals(Color.blue))
                buttonImg.color = Color.yellow;
            else if (!buttonImg.color.Equals(Color.blue))
                buttonImg.color = Color.white;
        }
        currentRow++;
        if (currentRow > 7) currentRow = 0;
    }
    #endregion Methods
}
