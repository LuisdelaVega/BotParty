using UnityEngine;
using UnityEngine.AI;

public class BotAgent : MonoBehaviour
{
    [Header("Bot Specific References")]
    [SerializeField] private Transform destination = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;
    [SerializeField] private Rigidbody m_rigidbody = null;


    private void Awake()
    {
        navMeshAgent.SetDestination(destination.position);
    }

    private void Update()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            navMeshAgent.isStopped = true;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            navMeshAgent.isStopped = false;
            m_rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}
