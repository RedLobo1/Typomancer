using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.1f, 3f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pitch = 1;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
    public bool playOnAwake;
    public bool randomPitch;
    public bool randomClip;
    public bool canInterrupt = true;
    public float minRandomPitch = 1, maxRandomPitch = 1;
    public List<AudioClip> randomClips;
}
