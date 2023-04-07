using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGameBehaviour : IGameManagerBehaviour
{
    float timeSpeed;
    public void RunBehaviour(bool enabled, GameObject pauseMenu)
    {
        pauseMenu.SetActive(enabled);
        Time.timeScale = timeSpeed;
    }
    public PauseGameBehaviour(float timeSpeed)
    {
        this.timeSpeed = timeSpeed;
    }
}
