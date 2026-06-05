using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // [Header("일지 UI")]
    // [SerializeField] GameObject journalPanel;
    // [SerializeField] TMP_Text journalTitleText;
    // [SerializeField] TMP_Text journalContentText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // void Update()
    // {
    //     if (journalPanel != null && journalPanel.activeSelf && Input.GetKeyDown(KeyCode.F))
    //         CloseJournal();
    // }

    public void ShowMessage(string message)
    {
    }

    public void UpdateInventoryUI()
    {
    }

    public void ShowJournal(string title, string content)
    {
        Debug.Log($"[일지] {title}\n{content}");
        // journalTitleText.text = title;
        // journalContentText.text = content;
        // journalPanel.SetActive(true);
    }

    public void CloseJournal()
    {
        Debug.Log("[일지] 닫힘");
        // journalPanel.SetActive(false);
    }

    [Header("자물쇠 UI")]
    [SerializeField] CombinationLockUI combinationLockUI;

    public void ShowCombinationLock(CombinationLockInteractable lockTarget)
    {
        if (combinationLockUI != null)
            combinationLockUI.Open(lockTarget);
        else
            Debug.Log("[자물쇠] UI 열림");
    }

    public void CloseCombinationLock()
    {
        if (combinationLockUI != null)
            combinationLockUI.Close();
        else
            Debug.Log("[자물쇠] UI 닫힘");
    }

    public void OnCombinationWrong()
    {
        Debug.Log("[자물쇠] 틀림");
        // TODO: 오답 피드백 (흔들림, 효과음 등)
    }
}
