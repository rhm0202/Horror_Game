using UnityEngine;

public class CircuitInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] string promptName = "회로";

    public string GetPromptName() => promptName;

    public void Interact()
    {
        UIManager.Instance.ShowFinalItemCheck();
    }
}
