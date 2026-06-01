using UnityEngine;

public class OpenDrawer : MonoBehaviour, IInteractable
{
    [Header("서랍 설정")]
    public float openDistance = 0.4f;
    public float openSpeed = 3f;

    [Header("열리는 방향 (보통 Z축)")]
    public Vector3 openDirection = Vector3.forward;

    [Header("서랍 안 아이템 (없으면 바로 닫기 가능)")]
    [SerializeField] ItemPickup[] drawerItems;

    [Header("서랍 콜라이더 (직접 연결)")]
    [SerializeField] Collider drawerCollider;

    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private Collider col;

    void Start()
    {
        closedPosition = transform.localPosition;
        openPosition = closedPosition + openDirection * openDistance;
        col = drawerCollider != null ? drawerCollider : GetComponent<Collider>();
    }

    void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * openSpeed);

        if (isOpen && col != null && !col.enabled)
        {
            bool anyItemActive = false;
            foreach (ItemPickup item in drawerItems)
            {
                if (item != null && item.gameObject.activeSelf)
                {
                    anyItemActive = true;
                    break;
                }
            }
            if (!anyItemActive) col.enabled = true;
        }
    }

    public void Interact()
    {
        isOpen = !isOpen;
        if (col != null) col.enabled = !isOpen;
    }
}