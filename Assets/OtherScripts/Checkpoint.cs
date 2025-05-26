using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    private bool playerInRange;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.SetCheckpoint(spawnPoint);
            Debug.Log("Checkpoint activated!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            UIManager.Instance.ShowCheckpointPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UIManager.Instance.ShowCheckpointPrompt(false);
        }
    }
}

