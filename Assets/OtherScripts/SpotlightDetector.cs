using UnityEngine;

public class SpotlightDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player caught by spotlight!");

            // Use GetComponentInParent to catch the player root
            PlayerLifeManager lifeManager = other.GetComponentInParent<PlayerLifeManager>();

            if (lifeManager != null)
            {
                lifeManager.TakeDamage();
            }
            else
            {
                Debug.LogError("PlayerLifeManager not found on or above the player collider!");
            }
        }
    }
}
