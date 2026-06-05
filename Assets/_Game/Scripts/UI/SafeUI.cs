using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafeUI : MonoBehaviour
{
    [Header("입력 표시 (입력한 숫자 순서 표시)")]
    [SerializeField] TMP_Text inputDisplayText;

    private List<int> currentSequence = new List<int>();
    private SafeInteractable currentSafe;
    private const int RequiredCount = 6;

    public void Open(SafeInteractable safe)
    {
        currentSafe = safe;
        Reset();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    // 버튼 OnClick에서 호출 (1~9)
    public void OnNumberPressed(int number)
    {
        if (currentSequence.Count >= RequiredCount) return;

        currentSequence.Add(number);
        UpdateDisplay();

        if (currentSequence.Count == RequiredCount)
            currentSafe.TryUnlock(currentSequence.ToArray());
    }

    public void OnReset()
    {
        Reset();
    }

    public void OnWrongFeedback()
    {
        Reset();
        if (inputDisplayText != null)
            inputDisplayText.text = "틀렸습니다";
    }

    private void Reset()
    {
        currentSequence.Clear();
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (inputDisplayText == null) return;

        if (currentSequence.Count == 0)
        {
            inputDisplayText.text = "- - - - - -";
            return;
        }

        inputDisplayText.text = string.Join(" ", currentSequence);
    }
}
