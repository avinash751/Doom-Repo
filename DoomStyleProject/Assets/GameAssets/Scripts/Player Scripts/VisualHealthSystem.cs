using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VisualHealthSystem : DynamicPostProcessing
{
    [SerializeField] PlayerHealth player;

    [SerializeField] float lerpInDuration;
    [SerializeField] float lerpOutDuration;

    [SerializeField] Volume meduimHealthVolume;
    [SerializeField] Volume lowHealthVolume;

    [SerializeField] float meduimHealthThreshold;
    [SerializeField] float lowHealthThreshold;

    [SerializeField] AudioSource meduimHealthSound;
    [SerializeField] AudioSource lowHealthSound;

    [SerializeField] float meduimHealthSoundVolume;
    [SerializeField] float lowHealthSoundVolume;

    [SerializeField] float lowHealthpitch;

    bool meduimHealthSoundPlayed;
    bool lowHealthSoundPlayed;





    private void Start()
    {
        player = player ?? GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        TransitionToLowHealthVolume();
        TransitionToMeduimHealthVolume();
    }

    void TransitionToMeduimHealthVolume()
    {
        bool CanTransitionToMeduimhealth = player.CurrentHealth <= meduimHealthThreshold && player.CurrentHealth > lowHealthThreshold;

        if (CanTransitionToMeduimhealth)
        {
            LerpBetweenTwoWeightsForAVolume(meduimHealthVolume, 1, lerpInDuration);
            PlayAnAudioSource(meduimHealthSound, ref meduimHealthSoundPlayed);
            FadeAnAudioSourceVolumeAndPitch(meduimHealthSound, meduimHealthSoundVolume, 1, lerpInDuration);
            return;
        }
        LerpBetweenTwoWeightsForAVolume(meduimHealthVolume, 0, lerpInDuration);
        FadeOutAnAudioSourceAndStopPlaying(meduimHealthSound,1, ref meduimHealthSoundPlayed);
    }

    private void TransitionToLowHealthVolume()
    {
        bool CanTransitionToLowHealth = player.CurrentHealth <= lowHealthThreshold;
        if (CanTransitionToLowHealth)
        {
            LerpBetweenTwoWeightsForAVolume(lowHealthVolume, 1, lerpInDuration);
            PlayAnAudioSource(lowHealthSound, ref lowHealthSoundPlayed);
            FadeAnAudioSourceVolumeAndPitch(lowHealthSound, lowHealthSoundVolume, lowHealthpitch, lerpInDuration);
            return;
        }
        LerpBetweenTwoWeightsForAVolume(lowHealthVolume, 0, lerpInDuration);
        FadeOutAnAudioSourceAndStopPlaying(lowHealthSound,1, ref lowHealthSoundPlayed);
    }

}
