using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBehaviour : IGameManagerBehaviour
{
    float timeSpeed;
    public void RunBehaviour(bool enabled, GameObject gameOverMenu)
    {
        gameOverMenu.SetActive(enabled);
        Time.timeScale = timeSpeed;
    }
    public GameOverBehaviour(float timeSpeed)
    {
        this.timeSpeed = timeSpeed;
    }
}
