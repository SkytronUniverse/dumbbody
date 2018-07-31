using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequencerAudio_v1 : MonoBehaviour {

    #region Variables

    private static int currentRow = 0;
    private static int currentButton = 0;

    public bool[,] audio_to_play;
    public AudioSource[] srcAudio;

    #endregion Variables

    #region Methods
    private void Start()
    {
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
        print("Executed: " + Time.time);
        int i = currentRow;

        for (int j = 0; j < 16; j++)
        {
            
            if (audio_to_play[i, j])
                srcAudio[j].Play();

            currentButton++;
            if (currentButton > 15) currentButton = 0;
        }
        currentRow++;
        if (currentRow > 7) currentRow = 0;
    }

    #endregion Methods
}
