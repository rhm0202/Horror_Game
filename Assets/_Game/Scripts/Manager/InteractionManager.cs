using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [SerializeField] Transform cameraTransform;
    [SerializeField] float interactRange = 1.5f;
    [SerializeField] LayerMask interactableLayer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (cameraTransform == null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.green);

        bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, interactRange, interactableLayer);
        IInteractable interactable = hit ? hitInfo.collider.GetComponentInParent<IInteractable>() : null;

        if (interactable != null)
        {
            string name = interactable is ItemPickup pickup
                ? pickup.GetItemName()
                : interactable.GetPromptName();
            UIManager.Instance?.ShowInteractPrompt(name);
        }
        else
        {
            UIManager.Instance?.HideInteractPrompt();
        }

        if (!Input.GetKeyDown(KeyCode.F)) return;
        if (!hit)
        {
            Debug.Log("[InteractionManager] 레이캐스트 미스");
            return;
        }

        Debug.Log($"[InteractionManager] 레이캐스트 히트: {hitInfo.collider.gameObject.name} / 레이어: {LayerMask.LayerToName(hitInfo.collider.gameObject.layer)}");

        if (interactable != null)
            interactable.Interact();
        else
            Debug.Log("[InteractionManager] IInteractable 없음");
    }
}
