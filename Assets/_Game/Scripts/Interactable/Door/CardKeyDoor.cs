using UnityEngine;

public class CardKeyDoor : MonoBehaviour, IInteractable
{
    [Header("프롬프트")]
    [SerializeField] string promptName = "카드키 문";

    [Header("카드키 설정")]
    [SerializeField] Item requiredCardKey;
    [SerializeField] string noKeyMessage = "카드키가 필요할 것 같다.";

    [Header("문 설정")]
    [SerializeField] DoorInteraction linkedDoor;

    private bool isUnlocked = false;

    public string GetPromptName() => promptName;

    public void Interact()
    {
        if (isUnlocked)
        {
            linkedDoor?.Interact();
            return;
        }

        if (!Inventory.Instance.HasItem(requiredCardKey))
        {
            UIManager.Instance.ShowDialogue(new string[] { noKeyMessage });
            return;
        }

        isUnlocked = true;
        linkedDoor?.UnlockAndOpen();
    }
}
