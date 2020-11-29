using System.Collections;
using UnityEngine;

public class DestinationTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<BotAgent>(out BotAgent botAgent))
            StartCoroutine(StopBotAgent(botAgent));
    }

    private IEnumerator StopBotAgent(BotAgent botAgent)
    {
        yield return new WaitForSeconds(Random.Range(0, 0.2f));
        botAgent.DestinationReached();
        botAgent.ShouldNavMeshAgentBeEnabled(false);
    }
}
