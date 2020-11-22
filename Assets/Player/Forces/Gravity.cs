using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private CharacterController controller = null;
    [SerializeField] protected MovementHandler movementHandler = null;

    [Header("Settings")]
    // This "makes the gravity really high" when we are on the ground so that the character goes down ramps smoothly
    [SerializeField] private float groundedPullMagnitude = 15f;

    private readonly float gravityMagnitude = Physics.gravity.y;

    private bool wasGroundedLastFrame;

    public Vector3 Value { get; private set; }

    private void OnEnable() => movementHandler.AddModifier(this);
    private void OnDisable() => movementHandler.RemoveModifier(this);

    private void Update() => ProcessGravity();

    private void ProcessGravity()
    {
        if (controller.isGrounded)
            Value = new Vector3(Value.x, -groundedPullMagnitude, Value.z);
        else if (wasGroundedLastFrame)  // This happens right at the moment we jump (We are no longer gorunded, but were last frame.
            Value = Vector3.zero;       // The idea is to not apply that "really high gravity" so the character can actually jump.
        else                            // Then, once we're in the air, we apply regular gravity. Also works when walking off a ledge.
            Value = new Vector3(Value.x, Value.y + gravityMagnitude * Time.deltaTime, Value.z);

        wasGroundedLastFrame = controller.isGrounded;
    }
}
