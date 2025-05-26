using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState { get; private set; }

    // Add this property to track crouching as a bool
    public bool IsCrouched { get; private set; }

    public void SetPlayerMovementState(PlayerMovementState playerMovementState)
    {
        CurrentPlayerMovementState = playerMovementState;
    }

    public void SetCrouchState(bool crouched)
    {
        IsCrouched = crouched;
    }
}

public enum PlayerMovementState
{
    Idling = 0,
    Walking = 1,
    Running = 2,
    Jumping = 3,
    Falling = 4,
    Sprinting = 5,
    Crouching = 6,
    Strafing = 7,
}
