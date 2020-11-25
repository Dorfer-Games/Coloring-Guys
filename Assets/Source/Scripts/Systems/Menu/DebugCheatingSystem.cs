using Kuhpik;
using UnityEngine;

public class DebugCheatingSystem : GameSystem, IIniting, IDisposing
{
    [SerializeField] GameObject cheatingPanel;
    [SerializeField] CheatSliderComponent moveSpeedSlider;
    [SerializeField] CheatSliderComponent rotationSpeedSlider;
    [SerializeField] CheatSliderComponent hexFallTimeSlider;
    [SerializeField] CheatSliderComponent hexFadeTimeSlider;

    float originalMoveSpeed;
    float originalRotationSpeed;
    float originalHexFallSpeed;
    float originalHexFadeSpeed;

    void IIniting.OnInit()
    {
#if DEBUG
        cheatingPanel.SetActive(true);

        moveSpeedSlider.Subscribe(1, 30, config.MoveSpeed, config.UpdateMoveSpeed);
        rotationSpeedSlider.Subscribe(1, 200, config.RotationSpeed, config.UpdateRotationSpeed);
        hexFallTimeSlider.Subscribe(0.1f, 10f, config.CellFallTime, config.UpdateCellFallTime);
        hexFadeTimeSlider.Subscribe(0.1f, 5f, config.CellFadeTime, config.UpdateCellFadeTime);

        originalMoveSpeed = config.MoveSpeed;
        originalRotationSpeed = config.RotationSpeed;
        originalHexFallSpeed = config.CellFallTime;
        originalHexFadeSpeed = config.CellFadeTime;
#endif
    }

    //Можно было бы это сделать в каком-то левом классе, тогда можно было бы не делать ресет.
    //Сейчас проблема в том, что я не могу поменять создать копию GameConfig и заменить его во всех системах.
    void IDisposing.OnDispose() 
    {
        config.UpdateMoveSpeed(originalMoveSpeed);
        config.UpdateRotationSpeed(originalRotationSpeed);
        config.UpdateCellFallTime(originalHexFallSpeed);
        config.UpdateCellFadeTime(originalHexFadeSpeed);
    }
}
