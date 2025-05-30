using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _soundsParents;

    public static SoundManager Instance;


    private void Awake()
    {
        Instance = this;

        SubscribeEvents();
    }
    private void OnDestroy()
    {
        UnsubscribeEvents();
    }


    public AudioSource GetAudio(string name)
    {
        AudioSource audio = _soundsParents.transform.Find(name).GetComponent<AudioSource>();

        return audio;
    }
    public void PlaySound(string name)
    {
        AudioSource audio = _soundsParents.transform.Find(name).GetComponent<AudioSource>();
        //if (!audio.isPlaying)
        audio.Play();
    }
    public void StopSound(string name)
    {
        _soundsParents.transform.Find(name).GetComponent<AudioSource>().Stop();
    }
    
    private void SubscribeEvents()
    {
        Transition.OnFadeTransition += () => PlaySound("Fade");
        BirdMovement.OnPlayerJumping += () => PlaySound("Jump");
        ButtonPress.OnButtonPressed += () => PlaySound("Click");
        GameManager.OnGameStateChanged += state => { if (state == GameState.Ending) PlaySound("Bump"); };
        ScoreManager.OnScoreIncreased += () => PlaySound("Score");
    }
    private void UnsubscribeEvents()
    {
        Transition.OnFadeTransition -= () => PlaySound("Fade");
        BirdMovement.OnPlayerJumping -= () => PlaySound("Jump");
        ButtonPress.OnButtonPressed -= () => PlaySound("Click");
        GameManager.OnGameStateChanged -= state => { if (state == GameState.Ending) PlaySound("Bump"); };
        ScoreManager.OnScoreIncreased -= () => PlaySound("Score");
    }
}
