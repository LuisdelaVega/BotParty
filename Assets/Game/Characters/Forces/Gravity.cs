﻿using UnityEngine;

public class Gravity : MonoBehaviour, IMovementModifier
{
    [Header("References")]
    [SerializeField] private CharacterController controller = null;
    [SerializeField] protected MovementManager movementManager = null;

    [Header("Settings")]
    // This makes the "gravity really strong" when the character is on the ground so that the it goes down ramps smoothly
    [SerializeField] private float groundedPullMagnitude = 15f;

    private readonly float gravityMagnitude = Physics.gravity.y;
    private bool wasGroundedLastFrame = true;

    public Vector3 Value { get; private set; }

    private void OnEnable() => movementManager.AddModifier(this);
    private void OnDisable() => movementManager.RemoveModifier(this);
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
