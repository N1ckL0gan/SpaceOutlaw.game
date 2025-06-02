using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public PlayerLocomotionInput playerInput; // Assigned via Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (playerInput != null)
            playerInput.allowLook = true; // Unlock it again
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (playerInput != null)
            playerInput.allowLook = false; // Lock the look input
    }

    public void RestartGame()
    {
        Debug.Log("Restarting Game...");

        Time.timeScale = 1f; // Resume time before restarting
        GameIsPaused = false;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        if (playerInput != null)
            playerInput.allowLook = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
