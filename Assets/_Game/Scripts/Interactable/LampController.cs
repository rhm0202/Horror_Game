using UnityEngine;

public class LampController : MonoBehaviour
{
    private Light[] lampLights;
    private bool isOn = false;

    [Header("Emission")]
    [SerializeField] private Renderer[] emissionRenderers;
    [SerializeField] private Color emissionColor = Color.white;
    [SerializeField] private float emissionIntensity = 3f;

    private void Awake()
    {
        lampLights = GetComponentsInChildren<Light>();

        // Renderer 자동 수집 (인스펙터에 없으면 자동으로)
        if (emissionRenderers == null || emissionRenderers.Length == 0)
            emissionRenderers = GetComponentsInChildren<Renderer>();
    }

    private void Start()
    {
        TurnOff();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            if (isOn) TurnOff();
            else TurnOn();

            isOn = !isOn;
        }
    }

    public void TurnOn()
    {
        foreach (Light l in lampLights)
            if (l != null) l.enabled = true;

        foreach (Renderer renderer in emissionRenderers)
        {
            if (renderer == null) continue;

            Material mat = renderer.sharedMaterial;
            mat.SetFloat("_EmissionEnabled", 1f);
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", emissionColor * emissionIntensity);
            mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
        }
    }

    public void TurnOff()
    {
        foreach (Light l in lampLights)
            if (l != null) l.enabled = false;

        foreach (Renderer renderer in emissionRenderers)
        {
            if (renderer == null) continue;

            Material mat = renderer.sharedMaterial;
            mat.SetFloat("_EmissionEnabled", 0f);
            mat.DisableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.black);
            mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
        }

        DynamicGI.UpdateEnvironment();
    }
}