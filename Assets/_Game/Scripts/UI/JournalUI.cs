using UnityEngine;
using UnityEngine.UI;

public class JournalUI : MonoBehaviour
{
    [SerializeField] Text dateText;
    [SerializeField] Text pageText;
    [SerializeField] Text titleText;
    [SerializeField] Text contentText;
    [SerializeField] Text authorText;

    [Header("페이지 이동")]
    [SerializeField] Button prevButton;
    [SerializeField] Button nextButton;

    string[] pages;
    int currentPage;
    string cachedDate;
    string cachedTitle;
    string cachedAuthor;

    void Awake()
    {
        if (prevButton != null) prevButton.onClick.AddListener(PrevPage);
        if (nextButton != null) nextButton.onClick.AddListener(NextPage);
    }

    public void Open(string date, string title, string[] contents, string author)
    {
        pages = contents;
        currentPage = 0;
        cachedDate = date;
        cachedTitle = title;
        cachedAuthor = author;
        ShowPage();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void PrevPage()
    {
        if (currentPage > 0) { currentPage--; ShowPage(); }
    }

    void NextPage()
    {
        if (currentPage < pages.Length - 1) { currentPage++; ShowPage(); }
    }

    void ShowPage()
    {
        if (dateText != null)    dateText.text    = cachedDate;
        if (titleText != null)   titleText.text   = cachedTitle;
        if (contentText != null) contentText.text = pages[currentPage];
        if (authorText != null)  authorText.text  = string.IsNullOrEmpty(cachedAuthor) ? "" : $"— {cachedAuthor}";
        if (pageText != null)    pageText.text     = pages.Length > 1 ? $"{currentPage + 1} / {pages.Length}" : "";

        if (prevButton != null) prevButton.gameObject.SetActive(currentPage > 0);
        if (nextButton != null) nextButton.gameObject.SetActive(currentPage < pages.Length - 1);
    }
}
