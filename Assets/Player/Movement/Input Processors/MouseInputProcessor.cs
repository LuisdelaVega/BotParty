using UnityEngine;
using Cinemachine;

public class MouseInputProcessor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float sensitivity = 0.15f;
    [SerializeField] private bool invertX = false;
    [SerializeField] private bool invertY = false;

    private Vector2 LookDelta;
    private Controls controls;

    private void Awake()
    {
        controls = new Controls();

        // Overrides
        Cursor.lockState = CursorLockMode.Locked;
        CinemachineCore.GetInputAxis = GetCustomInputAxis;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    public float GetCustomInputAxis(string axisName)
    {
        // Use the new input system
        LookDelta = controls.Player.Aim.ReadValue<Vector2>() * sensitivity;

        if (axisName == "Mouse X")
            return LookDelta.x * (invertX ? 1 : -1);
        else if (axisName == "Mouse Y")
            return LookDelta.y * (invertY ? 1 : -1);

        return 0;
    }
}
