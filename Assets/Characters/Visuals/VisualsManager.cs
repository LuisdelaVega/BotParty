using UnityEngine;
using UnityEngine.AI;

public class VisualsManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private CharacterVisuals visuals = null;

    public Animator animator { get; private set; }
    private bool isWalking = false;

    private void Awake() => animator = visuals.InstantiateVisuals(transform);
    private void Update()
    {
        if (characterMovement != null)
            isWalking = characterMovement.Value.magnitude > 0;
        else if (navMeshAgent != null)
            isWalking = navMeshAgent.enabled && !navMeshAgent.isStopped;

        animator.SetBool("isWalking", isWalking);
    }
}
