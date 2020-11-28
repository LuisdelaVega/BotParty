using UnityEngine;

[ExecuteAlways]
public class UINameTag : MonoBehaviour
{
    private Transform m_transform = null;
    private Transform gameplayCameraTransform;

    private void Awake() => m_transform = transform;
    void OnEnable() => gameplayCameraTransform = Camera.main.transform;
    void OnDisable() => gameplayCameraTransform = null;

    void LateUpdate()
    {
        if (gameplayCameraTransform)
            m_transform.LookAt(m_transform.position + gameplayCameraTransform.rotation * Vector3.forward, gameplayCameraTransform.rotation * Vector3.up);
    }
}