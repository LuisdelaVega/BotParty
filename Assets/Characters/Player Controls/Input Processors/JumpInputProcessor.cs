using UnityEngine;

public class JumpInputProcessor : ForceReceiver
{
    [SerializeField] private float jumpSpeed = 20f;

    private Controls controls;

    private void Awake() => controls = new Controls();

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Jump.performed += _ => Jump();
        movementHandler.AddModifier(this);
    }
    private void OnDisable()
    {
        controls.Disable();
        movementHandler.RemoveModifier(this);
    }

    private void Jump()
    {
        if (controller.isGrounded)
            AddForce(new Vector3(0f, jumpSpeed, 0f));
    }
}
