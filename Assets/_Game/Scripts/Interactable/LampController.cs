using UnityEngine;

public class LampController : MonoBehaviour
{
    // 모든 Light 자동 수집
    private Light[] lampLights;

    private bool isOn = false;

    [Header("Emission")]
    [SerializeField] private Renderer[] emissionRenderers;

    [SerializeField] private Color emissionColor = Color.white;
    [SerializeField] private float emissionIntensity = 3f;

    private void Awake()
    {
        // 자식 포함 모든 Light 가져오기
        lampLights = GetComponentsInChildren<Light>();
    }

    private void Start()
    {
        TurnOff();
    }

    // private void Update()
    // {
    //     // 🔥 숫자 0 키 누르면 토글
    //     if (Input.GetKeyDown(KeyCode.Alpha0))
    //     {
    //         if (isOn)
    //             TurnOff();
    //         else
    //             TurnOn();

    //         isOn = !isOn;
    //     }
    // }

    // 🔆 ON
    public void TurnOn()
    {
        foreach (Light l in lampLights)
        {
            if (l != null)
                l.enabled = true;
        }

        foreach (Renderer renderer in emissionRenderers)
        {
            Material mat = renderer.material;

            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor",
                emissionColor * emissionIntensity);
        }
    }

    // 🌑 OFF
    public void TurnOff()
    {
        foreach (Light l in lampLights)
        {
            if (l != null)
                l.enabled = false;
        }

        foreach (Renderer renderer in emissionRenderers)
        {
            Material mat = renderer.material;

            mat.SetColor("_EmissionColor", Color.black);
            mat.DisableKeyword("_EMISSION");
        }
    }
}