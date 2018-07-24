using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

    public AudioSource growl;
    public AudioSource drums;

    public void PlayAudioLoop()
    {
        growl.Play();
        drums.Play();
    }

    public void StopAudioLoop()
    {
        growl.Stop();
        drums.Stop();
    }
}
