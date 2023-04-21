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
        // this is an event to call gameover function when player health is 0
        PlayerHealth.PlayerHasDied += GameOverMenu;
        SetMouseCursor(CursorLockMode.Locked, true);
        SetTimeScale(1f);
    }
    void Update()
    {
        PauseMenu();
    }

    void PauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !paused && !playerNotDied)
        {
            EnablePauseMenu(0);
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && paused && !playerNotDied)
        {
            EnablePauseMenu(1);
        }
    }

    void GameOverMenu()
    {
        playerNotDied = true;
        currentGameBehaviour = new GameOverBehaviour(0);
        currentGameBehaviour.RunBehaviour(true, gameOverMenu);
        SetMouseCursor(CursorLockMode.None, true);
        Debug.Log("LOL YOU DIED NOOOB");
        PlayerHealth.PlayerHasDied -= GameOverMenu;
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

    public void   SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    public void EnablePauseMenu(float timeScale)
    {
        if(timeScale ==0)
        {
            paused = true;
            currentGameBehaviour = new PauseGameBehaviour(timeScale);
            currentGameBehaviour.RunBehaviour(true, pauseMenu);
            SetMouseCursor(CursorLockMode.None, true);
            return;
        }
        paused = false;
        currentGameBehaviour = new PauseGameBehaviour(timeScale);
        currentGameBehaviour.RunBehaviour(false, pauseMenu);
        SetMouseCursor(CursorLockMode.Locked,false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
