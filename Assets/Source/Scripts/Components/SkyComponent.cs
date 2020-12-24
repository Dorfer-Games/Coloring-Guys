using UnityEngine;

using System;



[Serializable]
public class SkySettings
{
    public Color topColor;
    public Color horizonColor;
    public Color bottomColor;
}

public class SkyComponent : MonoBehaviour
{
    [SerializeField] private Material sky;

    [Header("Цветовая настройка неба")]
    [SerializeField] private SkySettings[] skySettings;

    private void Start()
    {
        SetSettingsSky();
    }
    private void SetSettingsSky()
    {
        RenderSettings.skybox.SetColor("_Tint", skySettings[0].topColor);
        var block = new MaterialPropertyBlock();
        block.SetColor("Top Color", skySettings[0].topColor);
        block.SetVector("Horizon Color", skySettings[0].horizonColor);
        block.SetVector("Bottom Color", skySettings[0].bottomColor);
        
    }
}
