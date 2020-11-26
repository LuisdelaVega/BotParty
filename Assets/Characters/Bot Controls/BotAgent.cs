using UnityEngine;
using UnityEngine.AI;

public class BotAgent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform destination = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;

    private void Update()
    {
        navMeshAgent.SetDestination(destination.position);
    }
}
