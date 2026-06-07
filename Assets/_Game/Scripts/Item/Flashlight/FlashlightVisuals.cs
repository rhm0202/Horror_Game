using UnityEngine;

public class FlashlightVisuals : MonoBehaviour
{
    [SerializeField] GameObject flashlightOff;
    [SerializeField] GameObject flashlightOn;
    [SerializeField] GameObject flashlightUV;

    void Awake()
    {
        SetState(false, false);
    }

    void OnEnable()
    {
        ElectricTorchOnOff.OnFlashlightToggled += HandleToggle;
        ChooseLightCookie.OnUVModeChanged += HandleUVMode;
    }

    void OnDisable()
    {
        ElectricTorchOnOff.OnFlashlightToggled -= HandleToggle;
        ChooseLightCookie.OnUVModeChanged -= HandleUVMode;
    }

    void HandleToggle(bool isOn)
    {
        SetState(isOn, false);
    }

    void HandleUVMode(bool isUV)
    {
        flashlightOn.SetActive(!isUV);
        flashlightUV.SetActive(isUV);
        flashlightOff.SetActive(false);
    }

    void SetState(bool isOn, bool isUV)
    {
        flashlightOff.SetActive(!isOn);
        flashlightOn.SetActive(isOn && !isUV);
        flashlightUV.SetActive(isOn && isUV);
    }
}
