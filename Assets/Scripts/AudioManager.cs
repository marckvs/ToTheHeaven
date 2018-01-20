using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;



public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    private string isPlaying;

    public static AudioManager instance;

     

    // Use this for initialization
    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
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

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.nombre == name);
        s.source.Stop();
    }

    // Update is called once per frame 
    void Start()
    {

    }

    void Update()
    {
        if ((isPlaying != "MainLevel") && (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3))
        {
            Stop("Interface");
            Play("MainLevel");
            isPlaying = "MainLevel";


        }
        else if(isPlaying != "Interface" && (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1))
        {
            Stop("MainLevel");
            Play("Interface");
            isPlaying = "Interface";
        }
    }


}

