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

        if (!Input.GetKeyDown(KeyCode.F)) return;
        if (!Physics.Raycast(ray, out RaycastHit hit, interactRange, interactableLayer))
        {
            Debug.Log("[InteractionManager] 레이캐스트 미스");
            return;
        }

        Debug.Log($"[InteractionManager] 레이캐스트 히트: {hit.collider.gameObject.name} / 레이어: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");

        IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();
        if (interactable != null)
            interactable.Interact();
        else
            Debug.Log("[InteractionManager] IInteractable 없음");
    }
}
