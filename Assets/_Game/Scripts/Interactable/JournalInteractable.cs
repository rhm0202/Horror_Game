using UnityEngine;

public class JournalInteractable : MonoBehaviour, IInteractable
{
    [Header("일지 내용")]
    public string date = "";
    public string page = "";
    public string title = "";
    [TextArea(5, 20)]
    public string content = "";
    public string author = "";

    public string GetPromptName() => string.IsNullOrEmpty(title) ? "일지" : $"일지: {title}";

    public void Interact()
    {
        UIManager.Instance.ShowJournal(date, page, title, content, author);
    }
}
