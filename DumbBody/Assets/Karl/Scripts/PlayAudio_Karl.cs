using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace dumbbody_karl
{
    public class PlayAudio : MonoBehaviour
    {

        #region Variables
        int currentSynthPlaying;
        int currentDrumPlaying;
        float time = 0.0f;
        bool clicked = false;

        public int row;
        public int col;

        static Queue<AudioClip> drumQ;
        static Queue<AudioClip> growlQ;

        public AudioSource growl;
        public AudioSource drums;

        public Button growlButton;

        public AudioClip[] drumData = new AudioClip[2];
        public AudioClip[] growlData = new AudioClip[2];

        public AudioClip[,] audioData;
        public bool[,] loopsBool; //stores true or false of sound to play

        #endregion Variables

        #region Methods
        private void Start()
        {
            drumQ = new Queue<AudioClip>(2);
            growlQ = new Queue<AudioClip>(2);
            audioData = new AudioClip[2, 2];
            loopsBool = new bool[2, 2];

            for (int i = 0; i < loopsBool.Length; i++)
            {
                for (int j = 0; j < loopsBool.GetLength(i); j++)
                {
                    loopsBool[i, j] = false;
                }
            }
            audioData[0, 0] = drumData[0];
            audioData[0, 1] = drumData[1];
            audioData[1, 0] = growlData[0];
            audioData[1, 1] = growlData[1];

        }

        private void Update()
        {
            if(drumQ.Count != 0 && !drums.isPlaying)
            {
                drums.clip = drumQ.Dequeue();
                drums.Play();
            }
        }

        public void PlayDrumLoop(int buttonNum)
        {
            AudioClip drumClip = null;

            if (loopsBool[0, currentDrumPlaying] == true)
            {
                loopsBool[0, currentDrumPlaying] = false;
                GameObject.Find("Drums_" + currentDrumPlaying).GetComponent<Image>().color = Color.white;
            }
            int toSwitch = buttonNum;
            switch (toSwitch)
            {
                case 0: drumClip = drumData[0]; break;                       //drums.clip = drumData[0]; break;
                case 1: drumClip = drumData[1]; break;                        //drums.clip = drumData[1]; break;
            }
            loopsBool[0, buttonNum] = true;
            currentDrumPlaying = buttonNum;
            GameObject.Find("Drums_" + currentDrumPlaying).GetComponent<Image>().color = Color.red;
            drumQ.Enqueue(drumClip);
            //drums.loop = true;
            /*
            while (drumQ.Count != 0)
            {
                drums.clip = drumQ.Dequeue();
                drums.Play();
            }
            */
            
        }

        public void PlaySynthLoop(int buttonNum)
        {
            if (loopsBool[1, currentSynthPlaying] == true)
            {
                loopsBool[1, currentSynthPlaying] = false;
                GameObject.Find("Synth_" + currentSynthPlaying).GetComponent<Image>().color = Color.white;
            }

            int toSwitch = buttonNum;
            switch (toSwitch)
            {
                case 0: growl.clip = growlData[0]; break;
                case 1: growl.clip = growlData[1]; break;
            }
            loopsBool[1, buttonNum] = true;
            currentSynthPlaying = buttonNum;
            GameObject.Find("Synth_" + currentSynthPlaying).GetComponent<Image>().color = Color.green;
            growl.loop = true;
            growl.Play();
        }
        public void PushGrowl()
        {
            Debug.Log("Growl Length: " + growlData[0].length);
            growlQ.Enqueue(growlData[0]);
            clicked = true;
        }

        public void PlayAudioLoop()
        {
            while(drumQ.Count != 0 && drums.isPlaying)
            {
                drums.clip = drumQ.Dequeue();
                drums.Play();
            }
            if (!drums.isPlaying)
                drums.Play();

        }

        public void StopAudioLoop()
        {
            growl.Stop();
            drums.Stop();
            GameObject.Find("Synth_" + currentSynthPlaying).GetComponent<Image>().color = Color.white;
            GameObject.Find("Drums_" + currentDrumPlaying).GetComponent<Image>().color = Color.white;
        }
        #endregion Methods
    }
}
