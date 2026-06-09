using UnityEngine;
using UnityEngine.UI;

public class FlashlightBattery : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] ElectricTorchOnOff flashlight;
    [SerializeField] float drainDuration = 120f;

    CanvasGroup canvasGroup;
    float battery = 1f;
    bool isOn;
    bool isPickedUp;

    void Start()
    {
        canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    void OnEnable() => ElectricTorchOnOff.OnFlashlightToggled += HandleToggle;
    void OnDisable() => ElectricTorchOnOff.OnFlashlightToggled -= HandleToggle;

    void HandleToggle(bool on) => isOn = on;

    void Update()
    {
        if (!isPickedUp && Inventory.Instance != null && Inventory.Instance.HasFlashlight)
        {
            isPickedUp = true;
            canvasGroup.alpha = 1f;
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
