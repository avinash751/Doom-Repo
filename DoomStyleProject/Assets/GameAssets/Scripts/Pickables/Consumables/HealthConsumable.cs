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
        player.currentHealth += healthToAdd;
        player.currentHealth = Mathf.Clamp(player.currentHealth, 0, player.MaxHealth.value);
        Debug.Log("health added yay");
    }
}
