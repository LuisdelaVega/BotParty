using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private MovementManager movementManager = null;

    [Header("Settings")]
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected float acceleration = 100f;
    [SerializeField, Tooltip("Higher value = Slower turn rate")] protected float smoothTurnTime = 0.1f;

    private float currentSpeed = 0f;
    public float CurrentSpeed { get => currentSpeed; protected set => currentSpeed = value; }
    protected Vector3 previousVelocity = Vector3.zero;
    protected Vector3 previousDirection = Vector3.zero;
    protected float smoothTurnVelocity; // Used as a ref

    protected Transform m_transform = null;

    public Vector3 Value { get; protected set; }

    private void Awake()
    {
        m_transform = transform;
        AwakeHandler();
    }

    private void OnEnable() => movementManager.AddModifier(this);
    private void OnDisable() => movementManager.RemoveModifier(this);

    private void Update() => Move();

    protected void Rotate()
    {
        float targetAngle = Mathf.Atan2(Value.x, Value.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(m_transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
        m_transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    protected void SetMovementDirection(Vector3 direction) => previousDirection = direction;

    /* Abstract Methods*/
    protected abstract void AwakeHandler();
    protected abstract void Move();
}
