using UnityEngine;

public class JournalInteractable : MonoBehaviour, IInteractable
{
    [Header("일지 내용")]
    [TextArea(3, 10)]
    public string journalTitle = "일지";
    [TextArea(5, 20)]
    public string journalContent = "";

    public void Interact()
    {
        UIManager.Instance.ShowJournal(journalTitle, journalContent);
    }
}
