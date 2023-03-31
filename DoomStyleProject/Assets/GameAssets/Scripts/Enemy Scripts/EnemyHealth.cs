using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    [SerializeField] ParticleSystem explode;
    [SerializeField] AudioSource soundOnDestroy;
    public override void DestroyObject()
    {
        spawnParticleSystem(explode,2);
        PlayAnAudioSourceAndRandomizePitch(soundOnDestroy);
        Destroy(gameObject);
    }
}

