using UnityEngine;
using TMPro;

public class CombinationLockUI : MonoBehaviour
{
    [SerializeField] TMP_Text[] digitTexts;  // 숫자 텍스트 4개

    private int[] currentInput;
    private CombinationLockInteractable currentLock;

    void Awake()
    {
        currentInput = new int[4];
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

    public void OnUp(int index)
    {
        currentInput[index] = (currentInput[index] + 1) % 10;
        UpdateDisplay(index);
    }

    public void OnDown(int index)
    {
        currentInput[index] = (currentInput[index] + 9) % 10;
        UpdateDisplay(index);
    }

    public void OnConfirm()
    {
        if (currentLock != null)
            currentLock.TryUnlock(currentInput);
    }

    private void ResetDigits()
    {
        for (int i = 0; i < currentInput.Length; i++)
        {
            currentInput[i] = 0;
            UpdateDisplay(i);
        }
    }

    private void UpdateDisplay(int index)
    {
        if (digitTexts != null && index < digitTexts.Length && digitTexts[index] != null)
            digitTexts[index].text = currentInput[index].ToString();
    }
}
