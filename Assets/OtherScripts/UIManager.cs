using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject checkpointPrompt;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowCheckpointPrompt(bool show)
    {
        checkpointPrompt.SetActive(show);
    }
}
