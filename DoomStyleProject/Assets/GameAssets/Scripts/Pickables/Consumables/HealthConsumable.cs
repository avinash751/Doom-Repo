using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : Consumable
{
    PlayerHealth player;

    [SerializeField] int healthToAdd;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        
    }

    public override void Consume(float destroyTimer, AudioSource source)
    {
        base.Consume(destroyTimer, source);
        AddHealthToPlayer();
    }

    void AddHealthToPlayer()
    {
        player.CurrentHealth += healthToAdd;
        // clamping was removed since it is now doing it in the get and set of object health
    }
}
