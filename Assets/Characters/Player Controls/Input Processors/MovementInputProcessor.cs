using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputProcessor : CharacterMovement
{
    [Header("Player Specific Refereces")]
    [SerializeField] private CharacterController controller = null;

    private Transform mainCameraTransform = null;
    private bool isCruising = false;

    protected override void AwakeHandler() => mainCameraTransform = Camera.main.transform;
    public void OnMovement(InputAction.CallbackContext value) => SetMovementDirection(value.ReadValue<Vector2>());
    public void OnCruise(InputAction.CallbackContext value) => isCruising = value.ReadValue<float>() > 0;

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
