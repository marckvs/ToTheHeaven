using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound  {

    public string nombre;
    public AudioClip clip;

    [HideInInspector]

    public AudioSource source;
}
