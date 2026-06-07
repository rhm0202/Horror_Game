using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SafeUI : MonoBehaviour
{
    [SerializeField] TMP_Text inputDisplayText;

    private List<int> currentSequence = new List<int>();
    private IKeypadTarget currentTarget;

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

    public void OnNumberPressed(int number)
    {
        if (currentTarget == null) return;
        if (currentSequence.Count >= currentTarget.RequiredLength) return;

        currentSequence.Add(number);
        UpdateDisplay();

        if (currentSequence.Count == currentTarget.RequiredLength)
            currentTarget.TryUnlock(currentSequence.ToArray());
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
        inputDisplayText.text = currentSequence.Count == 0
            ? "- - - - - -"
            : string.Join(" ", currentSequence);
    }
}
