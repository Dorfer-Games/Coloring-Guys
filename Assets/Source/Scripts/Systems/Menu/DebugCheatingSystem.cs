using Kuhpik;
using UnityEngine;

public class DebugCheatingSystem : GameSystem, IIniting, IDisposing
{
    [SerializeField] GameObject cheatingPanel;
    [SerializeField] CheatSliderComponent moveSpeedSlider;
    [SerializeField] CheatSliderComponent rotationSpeedSlider;
    [SerializeField] CheatSliderComponent hexFallTimeSlider;
    [SerializeField] CheatSliderComponent hexFadeTimeSlider;
    [SerializeField] CheatSliderComponent hexBackTimeSlider;
    [SerializeField] CheatSliderComponent jumpStrengthSlider;
    [SerializeField] CheatSliderComponent gravityStrengthSlider;
    [SerializeField] CheatSliderComponent colorPerStackSlider;
    [SerializeField] CheatSliderComponent colorMaxSlider;

    float originalMoveSpeed;
    float originalRotationSpeed;
    float originalHexFallSpeed;
    float originalHexFadeSpeed;
    float originalHexBackSpeed;
    float originaljumpStrength;
    float originalGravityStrength;
    int originalColorPerStack;
    int originalColorMax;

    void IIniting.OnInit()
    {
#if DEBUG
        cheatingPanel.SetActive(true);

        moveSpeedSlider.Subscribe(1, 30, config.MoveSpeed, config.UpdateMoveSpeed);
        rotationSpeedSlider.Subscribe(1, 200, config.RotationSpeed, config.UpdateRotationSpeed);
        hexFallTimeSlider.Subscribe(0.1f, 10f, config.CellFallTime, config.UpdateCellFallTime);
        hexFadeTimeSlider.Subscribe(0.1f, 5f, config.CellFadeTime, config.UpdateCellFadeTime);
        hexBackTimeSlider.Subscribe(0.5f, 20f, config.CellBackTime, config.UpdateCellBackTime);
        jumpStrengthSlider.Subscribe(3, 30, config.JumpStrength, config.UpdateJumpStrength);
        gravityStrengthSlider.Subscribe(10, 80, config.GravityStrength, config.UpdateGravityStrenght);
        colorPerStackSlider.Subscribe(10, 100, config.ColorPerStack, config.UpdateColorPerStack);
        colorMaxSlider.Subscribe(10, 300, config.ColorMax, config.UpdateColorMax);

        originalMoveSpeed = config.MoveSpeed;
        originalRotationSpeed = config.RotationSpeed;
        originalHexFallSpeed = config.CellFallTime;
        originalHexFadeSpeed = config.CellFadeTime;
        originalHexBackSpeed = config.CellBackTime;
        originaljumpStrength = config.JumpStrength;
        originalGravityStrength = config.GravityStrength;
        originalColorPerStack = config.ColorPerStack;
        originalColorMax = config.ColorMax;
#endif
    }

    //Можно было бы это сделать в каком-то левом классе, тогда можно было бы не делать ресет.
    //Сейчас проблема в том, что я не могу поменять создать копию GameConfig и заменить его во всех системах.
    void IDisposing.OnDispose() 
    {
#if UNITY_EDITOR
        config.UpdateMoveSpeed(originalMoveSpeed);
        config.UpdateRotationSpeed(originalRotationSpeed);
        config.UpdateCellFallTime(originalHexFallSpeed);
        config.UpdateCellFadeTime(originalHexFadeSpeed);
        config.UpdateCellBackTime(originalHexBackSpeed);
        config.UpdateJumpStrength(originaljumpStrength);
        config.UpdateGravityStrenght(originalGravityStrength);
        config.UpdateColorPerStack(originalColorPerStack);
        config.UpdateColorMax(originalColorMax);
#endif
    }
}
