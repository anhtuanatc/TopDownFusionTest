using Fusion;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TopDownController : NetworkBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 15f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -20f;

    [Header("Physics")]
    [SerializeField] private float acceleration = 12f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField] private float airControl = 0.4f;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private CharacterController _controller;
    private Vector3 _velocity;
    private float _currentSpeed;
    private bool _isSprinting;

    public override void Spawned()
    {
        _controller = GetComponent<CharacterController>();
        _controller.center = Vector3.zero;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority) return;

        GroundCheck();
        HandleMovement();
        ApplyGravity();
        MoveCharacter();
    }

    private void GroundCheck()
    {
        bool wasGrounded = _controller.isGrounded;
        Vector3 checkPosition = transform.position + _controller.center;

        if (Physics.CheckSphere(checkPosition - new Vector3(0, _controller.height / 2, 0),
                             groundCheckRadius,
                             LayerMask.GetMask("Ground")))
        {
            if (!wasGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f; // Force pressing down on the ground
            }
        }
    }

    private void HandleMovement()
    {
        if (GetInput(out NetworkInputData input))
        {
            // Sprint system
            _isSprinting = input.Sprint;

            // Calculate smooth transition speed
            float targetSpeed = _isSprinting ? sprintSpeed : walkSpeed;
            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed,
                (input.Horizontal != 0 || input.Vertical != 0 ? acceleration : deceleration) * Runner.DeltaTime);

            // Character rotation processing
            Vector3 moveInput = new Vector3(input.Horizontal, 0, input.Vertical);
            if (moveInput.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(-input.Horizontal, -input.Vertical) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Runner.DeltaTime);
            }

            // Movement with air control
            Vector3 moveDirection = moveInput.normalized;
            float controlFactor = _controller.isGrounded ? 1f : airControl;
            _velocity.x = Mathf.Lerp(_velocity.x, moveDirection.x * _currentSpeed, controlFactor * Runner.DeltaTime * acceleration);
            _velocity.z = Mathf.Lerp(_velocity.z, moveDirection.z * _currentSpeed, controlFactor * Runner.DeltaTime * acceleration);

            // Jump
            if (_controller.isGrounded && input.Jump)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }

    private void ApplyGravity()
    {
        if (!_controller.isGrounded)
        {
            _velocity.y += gravity * Runner.DeltaTime;
        }
    }

    private void MoveCharacter()
    {
        _controller.Move(_velocity * Runner.DeltaTime);
    }

}