using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("문 설정")]
    public float openAngle = 90f;
    public float openSpeed = 3f;

    [Header("프롬프트")]
    [SerializeField] string promptName = "문";

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

    public string GetPromptName() => promptName;

    public void Interact()
    {
        if (isLocked)
        {
            if (requiredKey == null || !Inventory.Instance.HasItem(requiredKey))
            {
                Debug.Log("열쇠가 필요합니다.");

                // ↓ 잠긴 문 상호작용 효과음
                SFXManager.Instance.PlaySFX("door_locked");

                return;
            }
            isLocked = false;
            Inventory.Instance.RemoveItem(requiredKey);

            // ↓ 열쇠로 잠금 해제 효과음
            SFXManager.Instance.PlaySFX("door_unlock");
        }

        isOpen = !isOpen;

        // ↓ 문 열기 / 닫기 효과음
        SFXManager.Instance.PlaySFX(isOpen ? "door_open" : "door_close");
    }

    public void Unlock()
    {
        isLocked = false;
        Debug.Log($"[{gameObject.name}] 잠금 해제됨");

        // ↓ 외부에서 잠금 해제 시 효과음
        SFXManager.Instance.PlaySFX("door_unlock");
    }

    public void UnlockAndOpen()
    {
        isLocked = false;
        isOpen = true;

        // ↓ 외부에서 잠금 해제 + 열기 시 효과음
        SFXManager.Instance.PlaySFX("door_open");
    }
}