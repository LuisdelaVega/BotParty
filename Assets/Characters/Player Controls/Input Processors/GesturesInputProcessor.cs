using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMovement)), RequireComponent(typeof(VisualsManager))]
public class GesturesInputProcessor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement = null;
    [SerializeField] private VisualsManager visualsManager = null;

    private void PlayAnimation(string trigger, InputAction.CallbackContext value)
    {
        if (value.started && characterMovement.Value.magnitude == 0)
            visualsManager.animator.SetTrigger(trigger);
    }

    public void OnClap(InputAction.CallbackContext value) => PlayAnimation("Clap", value);
    public void OnAnnoyedHeadShake(InputAction.CallbackContext value) => PlayAnimation("Annoyed Head Shake", value);
}
