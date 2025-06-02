using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject winUI;
    public PlayerLocomotionInput playerInput; // Optional: lock look

    private void Start()
    {
        if (winUI != null)
            winUI.SetActive(false); // Hide on game start
    }

    public void ShowWinScreen()
    {
        if (winUI != null)
        {
            winUI.SetActive(true);
            Time.timeScale = 0f;

            if (playerInput != null)
                playerInput.allowLook = false;
        }
        else
        {
            Debug.LogWarning("WinUI is not assigned!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        if (winUI != null)
            winUI.SetActive(false);

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
