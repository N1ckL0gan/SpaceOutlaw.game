using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepSound;

    void Update()
    {
        // Check if any movement key is being held
        bool isWalking = Input.GetKey(KeyCode.W) ||
                         Input.GetKey(KeyCode.A) ||
                         Input.GetKey(KeyCode.S) ||
                         Input.GetKey(KeyCode.D);

        if (isWalking)
        {
            if (!footstepSound.isPlaying)
            {
                footstepSound.Play();
            }
        }
        else
        {
            footstepSound.Stop();
        }
    }
}
