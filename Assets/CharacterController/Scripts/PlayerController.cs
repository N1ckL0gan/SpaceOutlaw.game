using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;

    [Header("Movement Settings")]
    public float runAcceleration = 10f;
    public float runSpeed = 5f;
    public float sprintAcceleration = 0.5f;
    public float sprintSpeed = 7f;
    public float drag = 5f;
    public float gravity = 25f;
    public float jumpSpeed = 1f;
    public float movingThreshold = 0.01f;
    public bool IsActuallyGrounded => _characterController.isGrounded;

    [Header("Mouse Camera Settings")]
    public float lookSenseH = 0.1f;
    public float lookSenseV = 0.1f;
    public float lookLimitV = 80f;

    private PlayerLocomotionInput _playerLocomotionInput;
    private PlayerState _playerState;
    private Vector2 _cameraRotation = Vector2.zero;
    private float _targetYaw = 0f;
    private Vector3 _currentVelocity = Vector3.zero;
    private float _verticalVelocity = 0f;

    private void Awake()
    {
        _playerLocomotionInput = GetComponent<PlayerLocomotionInput>();
        _playerState = GetComponent<PlayerState>();

        if (_playerLocomotionInput == null)
        {
            Debug.LogError("PlayerLocomotionInput component is missing!");
        }
    }

    private void Update()
    {
        UpdateMovementState();
        HandleLateralMovement();
        HandleVerticalMovement();

        _currentVelocity.y = _verticalVelocity;
        _characterController.Move(_currentVelocity * Time.deltaTime);
    }

    private void HandleLateralMovement()
    {
        bool isSprinting = _playerState.CurrentPlayerMovementState == PlayerMovementState.Sprinting;
        bool isGrounded = _characterController.isGrounded;

        float lateralAcceleration = isSprinting ? sprintAcceleration : runAcceleration;
        float clampLateralMagnitude = isSprinting ? sprintSpeed : runSpeed;

        Vector3 cameraForwardXZ = new Vector3(_playerCamera.transform.forward.x, 0, _playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(_playerCamera.transform.right.x, 0, _playerCamera.transform.right.z).normalized;

        Vector3 moveDirection = cameraRightXZ * _playerLocomotionInput.moveInput.x +
                                cameraForwardXZ * _playerLocomotionInput.moveInput.y;

        Vector3 movementDelta = moveDirection * lateralAcceleration * Time.deltaTime;

        float airControlFactor = isGrounded ? 1f : 0.5f;
        _currentVelocity += movementDelta * airControlFactor;

        Vector3 lateralVelocity = new Vector3(_currentVelocity.x, 0, _currentVelocity.z);
        if (lateralVelocity.magnitude > clampLateralMagnitude)
        {
            lateralVelocity = lateralVelocity.normalized * clampLateralMagnitude;
            _currentVelocity.x = lateralVelocity.x;
            _currentVelocity.z = lateralVelocity.z;
        }

        if (isGrounded)
        {
            Vector3 dragForce = _currentVelocity.normalized * drag * Time.deltaTime;
            _currentVelocity = (_currentVelocity.magnitude > drag * Time.deltaTime)
                ? _currentVelocity - dragForce
                : Vector3.zero;
        }
    }

    private void HandleVerticalMovement()
    {
        bool isGrounded = _characterController.isGrounded;

        if (isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -10f;
        }

        _verticalVelocity -= gravity * Time.deltaTime;

        if (_playerLocomotionInput.JumpPressed && isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(jumpSpeed * 3f * gravity);
        }
    }

    private void UpdateMovementState()
    {
        bool isLateralMoving = IsmoveLateral();
        bool isSprinting = _playerLocomotionInput.SprintToggledOn && isLateralMoving;
        bool isGrounded = IsActuallyGrounded;

        if (isGrounded)
        {
            PlayerMovementState groundedState = isSprinting ? PlayerMovementState.Sprinting :
                                                  isLateralMoving ? PlayerMovementState.Running :
                                                  PlayerMovementState.Idling;

            _playerState.SetPlayerMovementState(groundedState);
        }
        else
        {
            _playerState.SetPlayerMovementState(_characterController.velocity.y >= 0f
                ? PlayerMovementState.Jumping
                : PlayerMovementState.Falling);
        }
    }

    private void LateUpdate()
    {
        _targetYaw += lookSenseH * _playerLocomotionInput.lookInput.x;

        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - lookSenseV * _playerLocomotionInput.lookInput.y, -lookLimitV, lookLimitV);

        transform.rotation = Quaternion.Euler(0f, _targetYaw, 0f);
        _playerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0f, 0f);
    }

    private bool IsmoveLateral()
    {
        Vector3 lateralVelocity = new Vector3(_characterController.velocity.x, 0f, _characterController.velocity.z);
        return lateralVelocity.magnitude > movingThreshold;
    }

    private void OnValidate()
    {
        if (_playerCamera == null)
        {
            _playerCamera = GetComponentInChildren<Camera>();
        }
    }
}
