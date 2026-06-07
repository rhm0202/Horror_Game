using UnityEngine;
using UnityEngine.UI;

public class JournalUI : MonoBehaviour
{
    [SerializeField] Text dateText;
    [SerializeField] Text pageText;
    [SerializeField] Text titleText;
    [SerializeField] Text contentText;
    [SerializeField] Text authorText;

    public void Open(string date, string page, string title, string content, string author)
    {
        if (dateText != null)   dateText.text   = date;
        if (pageText != null)   pageText.text   = page;
        if (titleText != null)  titleText.text  = title;
        if (contentText != null) contentText.text = content;
        if (authorText != null) authorText.text = string.IsNullOrEmpty(author) ? "" : $"— {author}";
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
