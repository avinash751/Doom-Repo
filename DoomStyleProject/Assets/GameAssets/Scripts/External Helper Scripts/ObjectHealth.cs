using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using System;


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
        //damageTakenSound?.Play();
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
}