using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDelayPlaySound : MonoBehaviour
{
    [Tooltip("this delayToFadeIn time only applies when sound is played at start ")][SerializeField]
    float delayTimOnStart;
    [SerializeField] bool playOnstart;
    [SerializeField] AudioSource audioSource;
    [SerializeField] UnityEvent doSomethingWhenSoundIsPlayed;

    void Start()
    {
        if(!playOnstart) return;
        InvokeEventAndSoundWithDelay(delayTimOnStart);
    }

    public void InvokeEventAndSoundWithDelay(float delayTime)
    {
        Invoke(nameof(PlaySoundAlongWithEvent), delayTime);
    }

    void PlaySoundAlongWithEvent()
    {
        PlaySound();
        doSomethingWhenSoundIsPlayed?.Invoke();
    }

    void PlaySound()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

}
