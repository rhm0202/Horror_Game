// - ChooseLightCookie - Script by Marcelli Michele

// This script is attached in primary model (default) of the Electric Torch.
// You can insert in the List any cookie light texture and choose it to be used in "Cookie" Light for Electric Torch
// It's possible to choose any letter on the keyboard to control the texture change

using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLightCookie : MonoBehaviour
{
    public static event Action<bool> OnUVModeChanged;
    public static void SetUVMode(bool active) => OnUVModeChanged?.Invoke(active);

    public void ResetToNormal()
    {
        _scroolList = 0;
        _thisLight.cookie = lightCookie[0];
        if (lightColors.Count > 0)
            _thisLight.color = lightColors[0];
        OnUVModeChanged?.Invoke(false);
    }

    public string chooseKeyForCookie = "R";
    private KeyCode _keyCode;
    [Space]
    public List<Texture> lightCookie = new List<Texture>();
    public List<Color> lightColors = new List<Color>();
    [Tooltip("UV 모드에 해당하는 lightColors 인덱스")]
    public int uvModeIndex = 1;
    private Light _thisLight;
    private int _scroolList = 0;

    void Awake()
    {
        _thisLight = GetComponent<Light>();
    }

     void Start()
    {
        _keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), chooseKeyForCookie);
    }

    void Update()
    {
        // detecting parse error keyboard type
        if (System.Enum.TryParse(chooseKeyForCookie, out _keyCode))
        {
            _keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), chooseKeyForCookie);
        }
        //

        ChooseCookie();
    }

    void ChooseCookie()
    {
        if (UIManager.Instance != null && UIManager.Instance.IsUIOpen) return;
        if (Input.GetKeyDown(_keyCode) && GetComponent<ElectricTorchOnOff>().IsOn)
        {
            _scroolList += 1;

            if (_scroolList >= lightCookie.Count)
            {
                _scroolList = 0;
            }

            _thisLight.cookie = lightCookie[_scroolList];
            if (_scroolList < lightColors.Count)
                _thisLight.color = lightColors[_scroolList];

            OnUVModeChanged?.Invoke(_scroolList == uvModeIndex);

        }
    }
}
