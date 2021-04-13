using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GamePaused = false;

    public GameObject pauseMenuUI;

    private InputManager inputManager;

    // Update is called once per frame
    void Awake()
    {
        inputManager = InputManager.Instance;


    }
    void Update()
    {
        if (inputManager.PlayerPaused() == true)
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        else
        {
            Debug.Log("PlayerPaused is false");
        }

    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;


    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Debug.Log("Game paused");


    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Restarted");
        Resume();
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game..");
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
