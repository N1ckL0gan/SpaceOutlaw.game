using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuCanvas; // Drag your Canvas object here in Inspector

    public void StartGame()
    {
        Debug.Log("Start Game Pressed");

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false); // Hide the Canvas
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
