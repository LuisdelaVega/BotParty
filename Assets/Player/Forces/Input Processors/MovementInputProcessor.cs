using UnityEngine;

public class MovementInputProcessor : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private MovementHandler movementHandler = null;
    [SerializeField] private Animator animator = null;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float acceleration = 100f;
    [SerializeField, Tooltip("Higher value = Slower turn rate")] private float smoothTurnTime = 0.1f;

    private float currentSpeed = 0f;
    private Vector3 previousVelocity = Vector3.zero;
    private Vector2 previousInputDirection = Vector2.zero;
    private float smoothTurnVelocity;

    private Transform m_transform = null;
    private Transform mainCameraTransform = null;
    private Controls controls;

    public Vector3 Value { get; private set; }

    private void Awake() => controls = new Controls();

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Movement.performed += ctx => SetMovementInput(ctx.ReadValue<Vector2>());
        movementHandler.AddModifier(this);
    }
    private void OnDisable()
    {
        controls.Disable();
        movementHandler.RemoveModifier(this);
    }

    private void Start()
    {
        m_transform = transform;
        mainCameraTransform = Camera.main.transform;
    }

    private void Update() => Move();

    public void SetMovementInput(Vector2 inputDirection) => previousInputDirection = inputDirection;

    private void Move()
    {
        float targetSpeed = movementSpeed * previousInputDirection.magnitude;

        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection;

        if (targetSpeed != 0f)
            movementDirection = forward * previousInputDirection.y + right * previousInputDirection.x;
        else
            movementDirection = previousVelocity.normalized;

        Value = movementDirection * currentSpeed;
        previousVelocity = new Vector3(controller.velocity.x, 0f, controller.velocity.z);
        currentSpeed = previousVelocity.magnitude;

        bool isWalking = currentSpeed > 0;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
            Rotate();
    }

    private void Rotate()
    {
        float targetAngle = Mathf.Atan2(Value.x, Value.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(m_transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
        m_transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
