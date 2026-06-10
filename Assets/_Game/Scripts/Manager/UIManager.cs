using UnityEngine;
using TMPro;

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

    void Update()
    {
        if (!IsUIOpen) return;
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (journalUI != null && journalUI.gameObject.activeSelf)
            CloseJournal();
        else if (safeUI != null && safeUI.gameObject.activeSelf)
            CloseSafe();
        else if (combinationLockUI != null && combinationLockUI.gameObject.activeSelf)
            CloseCombinationLock();
    }

    // void Update()
    // {
    //     if (journalPanel != null && journalPanel.activeSelf && Input.GetKeyDown(KeyCode.F))
    //         CloseJournal();
    // }

    [Header("HUD")]
    [SerializeField] GameObject hud;
    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactKeyText;
    [SerializeField] TMP_Text interactNameText;

    public void ShowInteractPrompt(string name)
    {
        if (IsUIOpen) return;
        if (interactPrompt != null) interactPrompt.SetActive(true);
        if (interactKeyText != null) interactKeyText.text = "[F]";
        if (interactNameText != null) interactNameText.text = name;
    }

    public void HideInteractPrompt()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
    }

    public bool IsUIOpen { get; private set; }

    void OpenUI()
    {
        IsUIOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (hud != null) hud.SetActive(false);
    }

    void CloseUI()
    {
        IsUIOpen = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (hud != null) hud.SetActive(true);
    }

    public void ShowMessage(string message)
    {
    }

    public void UpdateInventoryUI()
    {
    }

    [Header("일지 UI")]
    [SerializeField] JournalUI journalUI;

    public void ShowJournal(string date, string page, string title, string content, string author)
    {
        if (journalUI != null) journalUI.Open(date, page, title, content, author);
        OpenUI();
    }

    public void CloseJournal()
    {
        if (journalUI != null) journalUI.Close();
        CloseUI();
    }

    [Header("자물쇠 UI")]
    [SerializeField] CombinationLockUI combinationLockUI;

    public void ShowCombinationLock(CombinationLockInteractable lockTarget)
    {
        if (combinationLockUI != null)
            combinationLockUI.Open(lockTarget);
        else
            Debug.Log("[자물쇠] UI 열림");
        OpenUI();
    }

    public void CloseCombinationLock()
    {
        if (combinationLockUI != null)
            combinationLockUI.Close();
        else
            Debug.Log("[자물쇠] UI 닫힘");
        CloseUI();
    }

    public void OnCombinationWrong()
    {
        Debug.Log("[자물쇠] 틀림");
        // TODO: 오답 피드백 (흔들림, 효과음 등)
    }

    [Header("금고 UI")]
    [SerializeField] SafeUI safeUI;

    public void ShowSafe(IKeypadTarget safe)
    {
        if (safeUI != null) safeUI.Open(safe);
        else Debug.Log("[금고] UI 열림");
        OpenUI();
    }

    public void CloseSafe()
    {
        if (safeUI != null) safeUI.Close();
        else Debug.Log("[금고] UI 닫힘");
        CloseUI();
    }

    public void OnSafeWrong()
    {
        if (safeUI != null) safeUI.OnWrongFeedback();
        else Debug.Log("[금고] 틀림");
    }
}
