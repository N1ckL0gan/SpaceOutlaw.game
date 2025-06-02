using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public PlayerLocomotionInput playerInput; // Optional: lock look

    private void Start()
    {
        if (gameOverUI != null)
            gameOverUI.SetActive(false); // Hide on game start
    }

    public void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;

            if (playerInput != null)
                playerInput.allowLook = false;
        }
        else
        {
            Debug.LogWarning("GameOverUI is not assigned!");
        }
    }

    public void RestartGame()
    {
        // Resume time
        Time.timeScale = 1f;

        // Hide the Game Over UI if still active
        if (gameOverUI != null)
            gameOverUI.SetActive(false);

        // Re-enable look controls (if you disabled them)
        if (playerInput != null)
            playerInput.allowLook = true;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
