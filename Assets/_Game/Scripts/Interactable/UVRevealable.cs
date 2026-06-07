using UnityEngine;

public class UVRevealable : MonoBehaviour
{
    private Renderer[] renderers;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        SetVisible(false);
    }

    void OnEnable()
    {
        ChooseLightCookie.OnUVModeChanged += SetVisible;
    }

    void OnDisable()
    {
        ChooseLightCookie.OnUVModeChanged -= SetVisible;
    }

    void SetVisible(bool visible)
    {
        foreach (var r in renderers)
            r.enabled = visible;
    }
}
