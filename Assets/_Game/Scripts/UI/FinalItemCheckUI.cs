using UnityEngine;
using UnityEngine.UI;

public class FinalItemCheckUI : MonoBehaviour
{
    [System.Serializable]
    public class ItemEntry
    {
        public Item item;
        public Button button;
        public GameObject cover;
        public GameObject check;
    }

    [SerializeField] ItemEntry[] entries = new ItemEntry[3];
    [SerializeField] Button closeButton;

    [Header("게이지")]
    [SerializeField] Image gaugeFull;

    [Header("최종 버튼")]
    [SerializeField] Button finalButton;
    [SerializeField] GameObject buttonOff;

    [Header("복도 광원")]
    [SerializeField] Light[] corridorLights;

    [Header("탈출구 키패드")]
    [SerializeField] ExitKeypad exitKeypad;

    [Header("전력 복구 대사")]
    [SerializeField] string powerRestoredMessage = "전력이 복구됐다. 이제 나갈 수 있을 것 같다.";

    int checkedCount = 0;

    void Awake()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            int idx = i;
            if (entries[idx].button != null)
                entries[idx].button.onClick.AddListener(() => OnEntryClicked(idx));
        }
        if (closeButton != null)
            closeButton.onClick.AddListener(() => UIManager.Instance.CloseFinalItemCheck());
        if (finalButton != null)
            finalButton.onClick.AddListener(OnFinalButtonClicked);
    }

    void OnEntryClicked(int idx)
    {
        var entry = entries[idx];
        bool has = entry.item != null && Inventory.Instance.HasItem(entry.item);
        if (entry.cover != null) entry.cover.SetActive(!has);
        if (entry.check != null) entry.check.SetActive(has);
        if (has && entry.button != null)
        {
            entry.button.interactable = false;
            checkedCount++;
            UpdateGauge();
            if (checkedCount >= entries.Length)
                ActivateFinalButton();
        }
    }

    void UpdateGauge()
    {
        if (gaugeFull != null)
            gaugeFull.fillAmount = checkedCount / (float)entries.Length;
    }

    void ActivateFinalButton()
    {
        if (finalButton != null) finalButton.interactable = true;
        if (buttonOff != null) buttonOff.SetActive(false);
    }

    void OnFinalButtonClicked()
    {
        foreach (var light in corridorLights)
            if (light != null) light.enabled = true;

        if (exitKeypad != null) exitKeypad.enabled = true;

        UIManager.Instance.CloseFinalItemCheck();
        UIManager.Instance.ShowDialogue(new string[] { powerRestoredMessage });
    }

    bool initialized = false;

    public void Open()
    {
        gameObject.SetActive(true);
        if (!initialized)
        {
            checkedCount = 0;
            UpdateGauge();
            if (finalButton != null) finalButton.interactable = false;
            if (buttonOff != null) buttonOff.SetActive(true);
            initialized = true;
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
