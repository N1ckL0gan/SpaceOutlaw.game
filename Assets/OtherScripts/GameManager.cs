using UnityEngine;
using TMPro;  // Import TMPro namespace

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform startSpawnPoint;  // Assign this in the Inspector

    private Transform currentCheckpoint;
    private float playTime = 0f;

    [Header("UI")]
    public GameObject gameOverScreen;
    public TextMeshProUGUI timerText;  // Use TMP component here

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager instance assigned.");

            // Set start location as initial checkpoint
            if (startSpawnPoint != null)
                currentCheckpoint = startSpawnPoint;
            else
                Debug.LogWarning("Start spawn point not assigned in GameManager.");
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
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(playTime / 60);
            int seconds = Mathf.FloorToInt(playTime % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
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
