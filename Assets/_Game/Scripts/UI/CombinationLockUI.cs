using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombinationLockUI : MonoBehaviour
{
    [Header("숫자 표시")]
    [SerializeField] TMP_Text[] digitTexts;

    [Header("버튼 (인덱스 순서: 0~3)")]
    [SerializeField] Button[] upButtons;
    [SerializeField] Button[] downButtons;

    [Header("확인 버튼")]
    [SerializeField] Button confirmButton;

    int[] currentInput;
    CombinationLockInteractable currentLock;

    void Awake()
    {
        currentInput = new int[4];

        for (int i = 0; i < 4; i++)
        {
            int idx = i;
            if (upButtons != null && i < upButtons.Length && upButtons[idx] != null)
                upButtons[idx].onClick.AddListener(() => OnUp(idx));
            if (downButtons != null && i < downButtons.Length && downButtons[idx] != null)
                downButtons[idx].onClick.AddListener(() => OnDown(idx));
        }

        if (confirmButton != null)
            confirmButton.onClick.AddListener(OnConfirm);
    }

    public void Open(CombinationLockInteractable lockTarget)
    {
        currentLock = lockTarget;
        ResetDigits();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void OnUp(int index)
    {
        currentInput[index] = (currentInput[index] + 1) % 10;
        UpdateDisplay(index);
    }

    void OnDown(int index)
    {
        currentInput[index] = (currentInput[index] + 9) % 10;
        UpdateDisplay(index);
    }

    void OnConfirm()
    {
        if (currentLock != null)
            currentLock.TryUnlock(currentInput);
    }

    void ResetDigits()
    {
        for (int i = 0; i < currentInput.Length; i++)
        {
            currentInput[i] = 0;
            UpdateDisplay(i);
        }
    }

    void UpdateDisplay(int index)
    {
        if (digitTexts != null && index < digitTexts.Length && digitTexts[index] != null)
            digitTexts[index].text = currentInput[index].ToString();
    }
}
