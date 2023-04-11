using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : ObjectHealth
{
    [SerializeField] GameObject explode;
    [SerializeField] AudioSource soundOnDestroy;
    [SerializeField] float destroyTimer;
    public override void DestroyObject()
    {
        SpawnObject(explode, destroyTimer);
        PlayAnAudioSourceAndRandomizePitch(soundOnDestroy);
        gameObject.SetActive(false);
        Destroy(gameObject,destroyTimer);
    }
}

