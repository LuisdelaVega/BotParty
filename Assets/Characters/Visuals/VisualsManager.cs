using UnityEngine;
using UnityEngine.AI;

public class VisualsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovementInputProcessor movementInputProcessor = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private CharacterVisuals visuals = null;

    private Animator animator = null;
    private bool isWalking = false;

    private void Awake() => animator = visuals.InstantiateVisuals(transform);
    private void Update()
    {
        if (movementInputProcessor != null)
            isWalking = movementInputProcessor.CurrentSpeed > 0;
        else if (navMeshAgent != null)
            isWalking = !navMeshAgent.isStopped;

        animator.SetBool("isWalking", isWalking);
    }
}
