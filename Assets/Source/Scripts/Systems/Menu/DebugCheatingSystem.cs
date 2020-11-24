using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugCheatingSystem : GameSystem, IIniting, IDisposing
{
    [SerializeField] GameObject cheatingPanel;
    [SerializeField] Slider moveSpeedSlider;
    [SerializeField] Slider rotationSpeedSlider;

    [SerializeField] TextMeshProUGUI speedValueText;
    [SerializeField] TextMeshProUGUI rotationValueText;

    float originalMoveSpeed, originalRotationSpeed;

    void IIniting.OnInit()
    {
#if DEBUG
        cheatingPanel.SetActive(true);

        moveSpeedSlider.minValue = 1;
        moveSpeedSlider.maxValue = 30;
        moveSpeedSlider.value = config.MoveSpeed;
        moveSpeedSlider.onValueChanged.AddListener(OnSpeedChange);
        OnSpeedChange(config.MoveSpeed);
        originalMoveSpeed = config.MoveSpeed;

        rotationSpeedSlider.minValue = 1;
        rotationSpeedSlider.maxValue = 200;
        rotationSpeedSlider.value = config.RotationSpeed;
        rotationSpeedSlider.onValueChanged.AddListener(OnRotationChange);
        OnRotationChange(config.RotationSpeed);
        originalRotationSpeed = config.RotationSpeed;
#endif
    }

    void OnSpeedChange(float value)
    {
        config.UpdateMoveSpeed(value);
        speedValueText.text = value.ToString("F2");
    }

    void OnRotationChange(float value)
    {
        config.UpdateRotationSpeed(value);
        rotationValueText.text = value.ToString("F2");
    }

    void IDisposing.OnDispose()
    {
        config.UpdateMoveSpeed(originalMoveSpeed);
        config.UpdateRotationSpeed(originalRotationSpeed);
    }
}
