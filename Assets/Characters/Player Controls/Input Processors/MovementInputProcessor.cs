using UnityEngine;

public class MovementInputProcessor : CharacterMovement
{
    [Header("Player Specific Refereces")]
    [SerializeField] private CharacterController controller = null;

    private Transform mainCameraTransform = null;
    private Controls controls = null;

    private bool isCruising = false;

    protected override void AwakeHandler()
    {
        controls = new Controls();
        mainCameraTransform = Camera.main.transform;
    }

    protected override void OnEnableHandler()
    {
        controls.Enable();
        controls.Player.Movement.performed += ctx => SetMovementDirection(ctx.ReadValue<Vector2>());
        controls.Player.Cruise.performed += ctx => isCruising = ctx.ReadValue<float>() > 0;
    }
    protected override void OnDisableHandler() => controls.Disable();

    protected override void Move()
    {
        if (isCruising) return;

        float targetSpeed = movementSpeed * previousDirection.magnitude;

        CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection;

        if (targetSpeed != 0f)
            movementDirection = forward * previousDirection.y + right * previousDirection.x;
        else
            movementDirection = previousVelocity.normalized;

        Value = movementDirection * CurrentSpeed;
        previousVelocity = new Vector3(controller.velocity.x, 0f, controller.velocity.z);
        CurrentSpeed = previousVelocity.magnitude;

        if (CurrentSpeed > 0)
            Rotate();
    }
}
