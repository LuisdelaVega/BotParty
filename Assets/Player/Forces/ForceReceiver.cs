using UnityEngine;

public class ForceReceiver : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] protected CharacterController controller = null;
    [SerializeField] protected MovementHandler movementHandler = null;

    [Header("Settings")]
    [SerializeField] private float mass = 1f;
    [SerializeField, Tooltip("Dampens the force")] private float drag = 5f;

    private bool wasGroundedLastFrame;

    public Vector3 Value { get; private set; }

    private void Update()
    {
        if (!wasGroundedLastFrame && controller.isGrounded)
            Value = new Vector3(Value.x, 0f, Value.z);

        wasGroundedLastFrame = controller.isGrounded;

        if (Value.magnitude < 0.2f)
            Value = Vector3.zero;

        Value = Vector3.Lerp(Value, Vector3.zero, drag * Time.deltaTime);
    }

    public void AddForce(Vector3 force) => Value += force / mass;
}
