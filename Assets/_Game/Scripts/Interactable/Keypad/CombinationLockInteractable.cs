using UnityEngine;

public class CombinationLockInteractable : MonoBehaviour, IInteractable
{
    [Header("프롬프트")]
    [SerializeField] string promptName = "자물쇠";

    [Header("자물쇠 설정")]
    [SerializeField] int[] correctCombination = { 0, 0, 0, 0 };

    [Header("열릴 때 연출")]
    [SerializeField] Transform lockBody;
    [SerializeField] float openRiseAmount = 0.3f;
    [SerializeField] GameObject unlockedObject;

    [Header("잠금 해제될 문")]
    [SerializeField] DoorInteraction linkedDoor;

    private bool isUnlocked = false;

    public string GetPromptName() => promptName;

    public void Interact()
    {
        if (isUnlocked) return;
        UIManager.Instance.ShowCombinationLock(this);
    }

    public void TryUnlock(int[] input)
    {
        for (int i = 0; i < correctCombination.Length; i++)
        {
            if (input[i] != correctCombination[i])
            {
                UIManager.Instance.OnCombinationWrong();
                return;
            }
        }

        Unlock();
    }

    private void Unlock()
    {
        isUnlocked = true;
        UIManager.Instance.CloseCombinationLock();

        if (lockBody != null)
            lockBody.position += Vector3.up * openRiseAmount;

        if (unlockedObject != null)
            unlockedObject.SetActive(true);

        if (linkedDoor != null)
            linkedDoor.Unlock();

        gameObject.SetActive(false);
    }
}
