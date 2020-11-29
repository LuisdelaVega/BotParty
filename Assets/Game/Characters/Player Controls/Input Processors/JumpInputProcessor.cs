using UnityEngine;

public class JumpInputProcessor : ForceReceiver
{
    [SerializeField] private float jumpSpeed = 10f;

    private void OnEnable() => movementManager.AddModifier(this);
    private void OnDisable() => movementManager.RemoveModifier(this);

    public void OnJump()
    {
        if (controller.isGrounded)
            AddForce(new Vector3(0f, jumpSpeed, 0f));
    }
}
