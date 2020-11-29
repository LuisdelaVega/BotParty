using UnityEngine;

[CreateAssetMenu(fileName = "New Character Visual", menuName = "Character Visual")]
public class CharacterVisuals : ScriptableObject
{
    [SerializeField] private GameObject model = null;
    [SerializeField] private RuntimeAnimatorController animatorController = null;

    public Animator InstantiateVisuals(Transform parent)
    {
        Animator animator = Instantiate(model, parent).AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;

        return animator;
    }
}
