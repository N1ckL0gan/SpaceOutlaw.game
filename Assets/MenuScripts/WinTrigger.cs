using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public WinScreen winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winScreen != null)
                winScreen.ShowWinScreen();
            else
                Debug.LogWarning("WinScreen reference is missing in WinTrigger!");
        }
    }
}
