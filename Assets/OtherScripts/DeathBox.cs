using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the barrier is the player
        if (other.CompareTag("Player"))
        {
            PlayerLifeManager lifeManager = other.GetComponent<PlayerLifeManager>();
            if (lifeManager != null)
            {
                lifeManager.TakeDamage();  // Reduce life and respawn or game over
            }
            else
            {
                Debug.LogWarning("PlayerLifeManager component missing on player!");
            }
        }
    }
}
