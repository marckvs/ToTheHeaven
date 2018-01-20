using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    // Use this for initialization
    void Awake() {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.nombre == name);
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.nombre == name);
        s.source.Stop();
    }

    // Update is called once per frame 
    void Start () {
        Play("Interface");

    }
}
