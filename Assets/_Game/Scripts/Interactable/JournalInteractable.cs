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
    [TextArea(5, 20)]
    public string content3 = "";
    public string author = "";

    public string GetPromptName() => string.IsNullOrEmpty(title) ? "일지" : $"일지: {title}";

    public void Interact()
    {
        var pageList = new System.Collections.Generic.List<string> { content };
        if (!string.IsNullOrEmpty(content2)) pageList.Add(content2);
        if (!string.IsNullOrEmpty(content3)) pageList.Add(content3);
        UIManager.Instance.ShowJournal(date, title, pageList.ToArray(), author);
    }
}
