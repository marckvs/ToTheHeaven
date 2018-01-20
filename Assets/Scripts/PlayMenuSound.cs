using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayMenuSound : MonoBehaviour {

    private AudioManager audio;

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
    }

    public void PlayButtonSound()
    {
        audio.Play("Button");
    }

}
