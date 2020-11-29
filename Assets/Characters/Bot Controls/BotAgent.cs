using UnityEngine;
using UnityEngine.AI;

public class BotAgent : MonoBehaviour
{
    [Header("Bot Specific References")]
    [SerializeField] private Transform destination = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private NavMeshObstacle navMeshObstacle = null;

    private void Awake() => navMeshAgent.SetDestination(destination.position);

    private void Update()
    {
        if (destination == null) return;

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            navMeshAgent.isStopped = true;
        else
            navMeshAgent.isStopped = false;
    }

    public void DestinationReached()
    {
        destination = null;
        if (navMeshAgent.enabled)
            navMeshAgent.isStopped = true;
    }

    public void ShouldNavMeshAgentBeEnabled(bool enabled)
    {
        navMeshAgent.enabled = enabled;
        navMeshObstacle.enabled = !enabled;
    }
}
