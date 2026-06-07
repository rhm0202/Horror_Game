using UnityEngine;

public class ExitKeypad : MonoBehaviour, IInteractable, IKeypadTarget
{
    [Header("비밀번호")]
    [SerializeField] int[] correctPassword;
    public int RequiredLength => correctPassword.Length;

    [Header("열릴 문")]
    [SerializeField] DoorInteraction exitDoor;

    private bool isUnlocked = false;

    public void Interact()
    {
        if (isUnlocked) return;
        UIManager.Instance.ShowSafe(this);
    }

    public void TryUnlock(int[] input)
    {
        for (int i = 0; i < correctPassword.Length; i++)
        {
            if (input[i] != correctPassword[i])
            {
                UIManager.Instance.OnSafeWrong();
                return;
            }
        }

        isUnlocked = true;
        UIManager.Instance.CloseSafe();

        if (exitDoor != null)
            exitDoor.UnlockAndOpen();
    }
}
