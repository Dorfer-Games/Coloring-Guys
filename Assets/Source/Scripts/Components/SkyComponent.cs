using UnityEngine;

public class SkyComponent : MonoBehaviour
{
    [SerializeField] private Material[] sky;

    private void Start()
    {
        SetSettingsSky();
    }

    private void SetSettingsSky()
    {
        RenderSettings.skybox = sky[RandomSky()]; 
    }

    private int RandomSky()
    {
        return Random.Range(0,sky.Length);
    }
}
