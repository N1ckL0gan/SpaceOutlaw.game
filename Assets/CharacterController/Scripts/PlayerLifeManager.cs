using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    private void Start()
    {
        currentLives = maxLives;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateLives(currentLives); // Show at start
        }
        else
        {
            Debug.LogError("UIManager.Instance is null! Make sure UIManager is in the scene.");
        }
    }

    public void TakeDamage()
    {
        currentLives--; // Decrease lives first

        UIManager.Instance.UpdateLives(currentLives); // THEN update UI

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null! Cannot process damage.");
            return;
        }

        if (currentLives <= 0)
        {
            GameManager.Instance.ShowGameOver();
        }
        else
        {
            GameManager.Instance.RespawnPlayer(gameObject);
        }
    }

    public void ResetLives()
    {
        currentLives = maxLives; // Reset first
        UIManager.Instance.UpdateLives(currentLives); // Then update UI
    }
}
