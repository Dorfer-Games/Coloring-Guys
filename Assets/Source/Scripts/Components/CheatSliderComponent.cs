using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheatSliderComponent : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI titleText;
    [field: SerializeField] public EGameValue Type { get; private set; }

    GameValueConfig config;

    public bool Subscribe(GameValueConfig config)
    {
        this.config = config;

        slider.minValue = config.minmaxValue.x;
        slider.maxValue = config.minmaxValue.y;
        slider.value = config.value;
        slider.onValueChanged.AddListener(UpdateValue);

        UpdateValue(config.value);

        titleText.text = gameObject.name;

        //Быстрый хак для LINQ
        return false;
    }

    void UpdateValue(float value)
    {
        text.text = value.ToString("F1");
        config.value = value;
    }
}
