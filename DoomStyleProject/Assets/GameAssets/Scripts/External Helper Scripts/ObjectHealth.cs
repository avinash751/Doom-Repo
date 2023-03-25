using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;
using Random = UnityEngine.Random;
using UnityEditor.Graphs;


public class ObjectHealth : MonoBehaviour, IDamagable, IDestroyable
{
    public Value maxHealth;
    [SerializeField] int currentHealth;

    [SerializeField] AudioSource damageTakenSound;
    [SerializeField] public UnityEvent onDamageTaken;
    [SerializeField] public Action healthHasBeenChnaged;


    public virtual int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.value);
            healthHasBeenChnaged?.Invoke();
            if (currentHealth <= 0)
            {
                DestroyObject();
            }
        }
    }

    private void Awake()
    {
       InitialisingValues();
    }

    public void TakeDamage(int Amount)
    {
        CurrentHealth -= Amount;
        PlayAnAudioSourceAndRandomizePitch(damageTakenSound);
        onDamageTaken?.Invoke();
    }
    public virtual void DestroyObject()
    {
        Debug.Log("object is dead");
    }
    void InitialisingValues()
    {
        currentHealth = 0;
        currentHealth = maxHealth.value;
    }

    protected void PlayAnAudioSource(AudioSource audioSource)
    {
        if (audioSource == null) return;
        audioSource.Play();
    }

    protected void PlayAnAudioSourceAndRandomizePitch(AudioSource audioSource)
    {
        if (audioSource == null) return;
        audioSource.pitch = Random.Range(0.8f, 1.3f);
        audioSource.Play();
    }

    protected  void spawnParticleSystem(ParticleSystem particle,float destroyTimer)
    {
        if (particle == null) return;
        ParticleSystem particleDuplicate = Instantiate(particle, transform.position + Vector3.up, Quaternion.identity);
        Destroy(particle,destroyTimer);
    }
}