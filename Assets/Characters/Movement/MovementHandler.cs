using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private NavMeshAgent navMeshAgent = null;

    private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

    private void Update() => Move();
    public void AddModifier(IMovementModifier modifier) => modifiers.Add(modifier);
    public void RemoveModifier(IMovementModifier modifier) => modifiers.Remove(modifier);

    private void Move()
    {
        Vector3 movement = Vector3.zero;

        foreach(IMovementModifier modifier in modifiers)
            movement += modifier.Value;

        if (controller != null)
            controller.Move(movement * Time.deltaTime);
        else if (navMeshAgent != null)
            navMeshAgent.Move(movement * Time.deltaTime);
    }
}
