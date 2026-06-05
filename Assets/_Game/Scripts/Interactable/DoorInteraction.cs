using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("문 설정")]
    public float openAngle = 90f;
    public float openSpeed = 3f;

    [Header("잠금 설정")]
    public bool isLocked = false;
    public Item requiredKey;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
    }

    public void Interact()
    {
        if (isLocked)
        {
            if (requiredKey == null || !Inventory.Instance.HasItem(requiredKey))
            {
                Debug.Log("열쇠가 필요합니다.");
                return;
            }
            isLocked = false;
            Inventory.Instance.RemoveItem(requiredKey);
        }

        isOpen = !isOpen;
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log($"[{gameObject.name}] 잠금 해제됨");
    }
}