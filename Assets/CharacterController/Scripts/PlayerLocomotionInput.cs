using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-2)]
public class PlayerLocomotionInput : MonoBehaviour, PlayerControls.IPlayerLocomotionMapActions
{
    [SerializeField] private bool holdToSprint = true;

    public PlayerControls playerControls { get; private set; }
    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool SprintToggledOn { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool Crouch { get; private set; }

    private void Awake()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
        }

        playerControls.Enable();
        playerControls.PlayerLocomotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.PlayerLocomotionMap.RemoveCallbacks(this);
            playerControls.Disable();
        }
    }

    public void OnWASD(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJoystick(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnToggleSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!holdToSprint)
            {
                SprintToggledOn = !SprintToggledOn;
            }
        }
        else if (context.canceled && holdToSprint)
        {
            SprintToggledOn = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpPressed = true;
        }
        else if (context.canceled)
        {
            JumpPressed = false;
        }
    }
}
