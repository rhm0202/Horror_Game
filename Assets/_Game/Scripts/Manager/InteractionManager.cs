using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; private set; }

    [SerializeField] Transform cameraTransform;
    [SerializeField] float interactRange = 2.5f;
    [SerializeField] string interactKey = "f";
    [SerializeField] LayerMask itemLayer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!Input.GetKeyDown(interactKey.ToLower())) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, interactRange, itemLayer)) return;

        if (hit.collider.TryGetComponent(out ItemPickup pickup))
            pickup.Pickup();
    }
}
