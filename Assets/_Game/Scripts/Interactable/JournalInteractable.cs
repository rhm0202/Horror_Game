using UnityEngine;

public class JournalInteractable : MonoBehaviour, IInteractable
{
    [Header("일지 내용")]
    public string date = "";
    public string title = "";
    [TextArea(5, 20)]
    public string content = "";
    [TextArea(5, 20)]
    public string content2 = "";
    public string author = "";

    public string GetPromptName() => string.IsNullOrEmpty(title) ? "일지" : $"일지: {title}";

    public void Interact()
    {
        var pages = string.IsNullOrEmpty(content2)
            ? new string[] { content }
            : new string[] { content, content2 };
        UIManager.Instance.ShowJournal(date, title, pages, author);
    }
}
