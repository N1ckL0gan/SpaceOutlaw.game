using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    private void Start()
    {
        currentLives = maxLives;
    }

    public void TakeDamage()
    {
        currentLives--;

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
        currentLives = maxLives;
    }
}
