using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private MovementHandler movementHandler = null;

    [Header("Settings")]
    [SerializeField] protected float movementSpeed = 3f;
    [SerializeField] protected float acceleration = 100f;
    [SerializeField, Tooltip("Higher value = Slower turn rate")] protected float smoothTurnTime = 0.1f;

    private float currentSpeed = 0f;
    public float CurrentSpeed { get => currentSpeed; protected set => currentSpeed = value; }
    protected Vector3 previousVelocity = Vector3.zero;
    protected Vector2 previousDirection = Vector2.zero;
    protected float smoothTurnVelocity; // Used as a ref

    protected Transform m_transform = null;
    protected Transform mainCameraTransform = null;

    public Vector3 Value { get; protected set; }

    private void Awake()
    {
        m_transform = transform;
        OnAwakeHandler();
    }

    private void OnEnable()
    {
        movementHandler.AddModifier(this);
        OnEnableHandler();
    }
    private void OnDisable()
    {
        movementHandler.RemoveModifier(this);
        OnDisableHandler();
    }

    private void Update() => Move();

    protected void Rotate()
    {
        float targetAngle = Mathf.Atan2(Value.x, Value.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(m_transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, smoothTurnTime);
        m_transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    /* Abstract Methods*/
    protected abstract void OnAwakeHandler();
    protected abstract void OnEnableHandler();
    protected abstract void OnDisableHandler();
    protected abstract void Move();
    protected abstract void SetMovementDirection(Vector2 direction);// => previousDirection = direction;
}
