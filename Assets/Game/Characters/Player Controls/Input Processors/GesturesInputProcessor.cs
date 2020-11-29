using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(CharacterMovement)), RequireComponent(typeof(VisualsManager))]
public class GesturesInputProcessor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject gestureWheelParent = null;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook = null;
    [SerializeField] private CharacterMovement characterMovement = null;
    [SerializeField] private VisualsManager visualsManager = null;

    private void Start() => ShouldWheelBeEnabled(false);

    private void PlayAnimation(string trigger, InputAction.CallbackContext value)
    {
        if (value.started && characterMovement.Value.magnitude == 0)
            visualsManager.animator.SetTrigger(trigger);
    }

    public void OnGestureWheel(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            ShouldWheelBeEnabled(true);
            ShouldCameraBeDisabled(false);
        }
        else if (value.canceled)
        {
            ShouldWheelBeEnabled(false);
            ShouldCameraBeDisabled(true);
        }
    }

    public void ShouldWheelBeEnabled(bool value)
    {
        if (gestureWheelParent != null)
            gestureWheelParent.SetActive(value);
    }

    private void ShouldCameraBeDisabled(bool value)
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.Confined;
        if (cinemachineFreeLook != null)
        {
            cinemachineFreeLook.enabled = value;
        }
    }

    public void OnApplause(InputAction.CallbackContext value) => PlayAnimation("Applause", value);
    public void OnAnnoyedHeadShake(InputAction.CallbackContext value) => PlayAnimation("Annoyed Head Shake", value);
}
