using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] float charDelay = 0.05f;

    [TextArea(2, 5)]
    [SerializeField] string[] lines;

    bool isTyping = false;
    bool advance = false;
    bool skip = false;
    bool playInitiated = false;

    void Start()
    {
        if (playInitiated) return;
        if (UIManager.Instance != null) { UIManager.Instance.IsUIOpen = true; UIManager.Instance.SetHUD(false); }
        StartCoroutine(PlayDialogue());
    }

    public void Play(string[] newLines)
    {
        StopAllCoroutines();
        playInitiated = true;
        lines = newLines;
        isTyping = false;
        advance = false;
        skip = false;
        if (UIManager.Instance != null) { UIManager.Instance.IsUIOpen = true; UIManager.Instance.SetHUD(false); }
        gameObject.SetActive(true);
        StartCoroutine(PlayDialogue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (isTyping) skip = true;
            else advance = true;
        }
    }

    IEnumerator PlayDialogue()
    {
        foreach (var line in lines)
        {
            advance = false;
            skip = false;
            yield return StartCoroutine(TypeLine(line));
            yield return new WaitUntil(() => advance);
        }

        if (UIManager.Instance != null) { UIManager.Instance.IsUIOpen = false; UIManager.Instance.SetHUD(true); }
        gameObject.SetActive(false);
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            if (skip)
            {
                dialogueText.text = line;
                break;
            }
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(charDelay);
        }

        isTyping = false;
    }
}
