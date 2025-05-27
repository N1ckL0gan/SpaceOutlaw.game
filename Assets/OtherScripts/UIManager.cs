using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public TextMeshProUGUI livesText;  // Use TMP for lives display

    // For checkpoint prompt, if it’s a panel or any GameObject (e.g. a UI panel or text)
    public GameObject checkpointPromptUI;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateLives(int lives)
    {
        if (livesText != null)
            livesText.text = $"Lives: {lives}";
    }

    public void ShowCheckpointPrompt(bool show)
    {
        if (checkpointPromptUI != null)
            checkpointPromptUI.SetActive(show);
    }
}
