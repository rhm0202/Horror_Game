using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafeUI : MonoBehaviour
{
    [Header("숫자 표시")]
    [SerializeField] TMP_Text inputDisplayText;

    [Header("버튼 (인덱스 = 숫자값, 0~9)")]
    [SerializeField] Button[] numberButtons;

    [Header("리셋 버튼")]
    [SerializeField] Button resetButton;

    List<int> currentSequence = new List<int>();
    IKeypadTarget currentTarget;

    void Awake()
    {
        for (int i = 0; i < numberButtons.Length; i++)
        {
            int num = i;
            if (numberButtons[num] != null)
                numberButtons[num].onClick.AddListener(() => OnNumberPressed(num));
        }

        if (resetButton != null)
            resetButton.onClick.AddListener(OnReset);
    }

    public void Open(IKeypadTarget target)
    {
        currentTarget = target;
        Reset();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    void OnNumberPressed(int number)
    {
        if (currentTarget == null) return;
        if (currentSequence.Count >= currentTarget.RequiredLength) return;

        currentSequence.Add(number);
        UpdateDisplay();

        if (currentSequence.Count == currentTarget.RequiredLength)
            currentTarget.TryUnlock(currentSequence.ToArray());
    }

    void OnReset()
    {
        Reset();
    }

    public void OnWrongFeedback()
    {
        Reset();
        if (inputDisplayText != null)
            inputDisplayText.text = "틀렸습니다";
    }

    void Reset()
    {
        currentSequence.Clear();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (inputDisplayText == null) return;
        inputDisplayText.text = currentSequence.Count == 0
            ? "- - - - - -"
            : string.Join(" ", currentSequence);
    }
}
