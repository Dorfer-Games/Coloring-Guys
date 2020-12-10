using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheatSliderComponent : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void Subscribe(float min, float max, float value, UnityAction<float> callBack)
    {
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = value;
        slider.onValueChanged.AddListener(callBack);
        slider.onValueChanged.AddListener(UpdateValue);

        UpdateValue(value);
    }

    void UpdateValue(float value)
    {
        text.text = value.ToString("F1");
    }
}
