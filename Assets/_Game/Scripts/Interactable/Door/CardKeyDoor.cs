using UnityEngine;

public class CardKeyDoor : MonoBehaviour, IInteractable
{
    [Header("카드키 설정")]
    [SerializeField] Item requiredCardKey;

    [Header("문 설정")]
    [SerializeField] DoorInteraction linkedDoor;

    private bool isUnlocked = false;

    public void Interact()
    {
        if (isUnlocked)
        {
            linkedDoor?.Interact();
            return;
        }

        if (!Inventory.Instance.HasItem(requiredCardKey))
        {
            Debug.Log("카드키가 필요합니다.");
            return;
        }

        isUnlocked = true;
        linkedDoor?.UnlockAndOpen();
        gameObject.SetActive(false);
    }
}
