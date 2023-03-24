using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : VisulaBar
{
    PlayerHealth player;

    public override void Start()
    {
        base.Start();
        initialisingHealthbarValues();   
    }


    void initialisingHealthbarValues()
    {
        player = FindObjectOfType<PlayerHealth>();
        SetNewCurrentValueAndMaxValueAndUpdateBar(player.CurrentHealth, player.maxHealth.value);
        player.healthHasBeenChnaged += UpdateHealthbar;
    }


    void UpdateHealthbar()
    {
        DecreaseCircularBarValue(player.CurrentHealth);
    }

}

