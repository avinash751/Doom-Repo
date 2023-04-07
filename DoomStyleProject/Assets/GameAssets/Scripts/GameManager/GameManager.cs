using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    IGameManagerBehaviour currentGameBehaviour;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gameOverMenu;

    bool paused;
    bool playerNotDied;
    private void Start()
    {
        PlayerHealth.PlayerHasDied += GameOverMenu;
        SetMouseCursor(CursorLockMode.Locked, true);
        Time.timeScale = 1f;
    }
    void Update()
    {
        PauseMenu();
    }

    void PauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused && !playerNotDied)
        {
            paused = true;
            currentGameBehaviour = new PauseGameBehaviour(0);
            currentGameBehaviour.RunBehaviour(true, pauseMenu);
            SetMouseCursor(CursorLockMode.None, true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused && !playerNotDied)
        {
            paused = false;
            currentGameBehaviour = new PauseGameBehaviour(1);
            currentGameBehaviour.RunBehaviour(false, pauseMenu);
            SetMouseCursor(CursorLockMode.Locked, false);
        }
    }

    void GameOverMenu()
    {
        playerNotDied = true;
        currentGameBehaviour = new GameOverBehaviour(0f);
        currentGameBehaviour.RunBehaviour(enabled, gameOverMenu);
        SetMouseCursor(CursorLockMode.None, true);
        Debug.Log("LOL YOU DIED NOOOB");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    void SetMouseCursor(CursorLockMode cursorState, bool enabled)
    {
        Cursor.lockState = cursorState;
        Cursor.visible = enabled;
    }
}
