using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance;
    private void Awake()
    {
        Instance = this;
    }
    public AudioSource GetVoice(string name)
    {
        AudioSource voice = GameObject.Find("Voice").transform.Find(name).GetComponent<AudioSource>();

        return voice;
    }
    public void PlayVoice(string name)
    {
        AudioSource voice = GameObject.Find("Voice").transform.Find(name).GetComponent<AudioSource>();
        //if (!voice.isPlaying)
        voice.Play();
    }
    public void StopVoice(string name)
    {
        GameObject.Find("Voice").transform.Find(name).GetComponent<AudioSource>().Stop();
    }
    public void StopAllSounds()
    {
        /*
        StopVoice("Walking");
        StopVoice("Swipe");
        */
    }
}
