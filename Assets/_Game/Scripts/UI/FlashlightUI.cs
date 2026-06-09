using UnityEngine;
using UnityEngine.UI;

public class FlashlightUI : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] ElectricTorchOnOff flashlight;
    [SerializeField] GameObject flashlightIcon;
    [SerializeField] GameObject batteryBar;
    [SerializeField] float drainDuration = 120f;

    float battery = 1f;
    bool isOn;

    void Start()
    {
        if (flashlightIcon != null) flashlightIcon.SetActive(false);
        if (batteryBar != null) batteryBar.SetActive(false);
    }

    void OnEnable() => ElectricTorchOnOff.OnFlashlightToggled += HandleToggle;
    void OnDisable() => ElectricTorchOnOff.OnFlashlightToggled -= HandleToggle;

    void HandleToggle(bool on) => isOn = on;

    void Update()
    {
        if (Inventory.Instance != null && Inventory.Instance.HasFlashlight)
        {
            if (flashlightIcon != null && !flashlightIcon.activeSelf) flashlightIcon.SetActive(true);
            if (batteryBar != null && !batteryBar.activeSelf) batteryBar.SetActive(true);
        }

        if (!isOn || battery <= 0f) return;

        battery -= Time.deltaTime / drainDuration;
        battery = Mathf.Max(battery, 0f);
        fillImage.fillAmount = battery;

        if (battery <= 0f)
            flashlight.ForceOff();
    }

    public void AddBattery(float amount)
    {
        battery = Mathf.Min(battery + amount, 1f);
        fillImage.fillAmount = battery;
    }
}
