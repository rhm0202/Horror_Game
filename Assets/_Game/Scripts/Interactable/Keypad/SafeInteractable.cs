using UnityEngine;

public class SafeInteractable : MonoBehaviour, IInteractable, IKeypadTarget
{
    [Header("프롬프트")]
    [SerializeField] string promptName = "보관함";

    [Header("금고 설정")]
    [SerializeField] int[] correctSequence = { 1, 2, 3, 4, 5, 6 };
    public int RequiredLength => correctSequence.Length;

    [Header("잠금 해제될 문")]
    [SerializeField] DoorInteraction linkedDoor;

    private bool isUnlocked = false;

    public string GetPromptName() => promptName;

    public void Interact()
    {
        if (isUnlocked) return;
        UIManager.Instance.ShowSafe(this);
    }

    public void TryUnlock(int[] input)
    {
        for (int i = 0; i < correctSequence.Length; i++)
        {
            if (input[i] != correctSequence[i])
            {
                UIManager.Instance.OnSafeWrong();
                return;
            }
        }

        Unlock();
    }

    private void Unlock()
    {
        isUnlocked = true;
        UIManager.Instance.CloseSafe();

        if (linkedDoor != null)
            linkedDoor.UnlockAndOpen();

        gameObject.SetActive(false);
    }
}
