using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private Transform currentCheckpoint;
    private float playTime = 0f;

    [Header("UI")]
    public GameObject gameOverScreen;
    public Text timerText;

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager instance assigned.");
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Duplicate GameManager destroyed.");
        }
    }


    void Update()
    {
        if (!isGameOver)
        {
            playTime += Time.deltaTime;
        }
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        currentCheckpoint = checkpoint;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (currentCheckpoint != null)
        {
            player.transform.position = currentCheckpoint.position;
        }
        else
        {
            Debug.LogWarning("No checkpoint set.");
        }
    }

    public void ShowGameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;

        int minutes = Mathf.FloorToInt(playTime / 60);
        int seconds = Mathf.FloorToInt(playTime % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}";
    }
}
