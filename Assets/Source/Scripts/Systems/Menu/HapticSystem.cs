using MoreMountains.NiceVibrations;

using UnityEngine;

public class HapticSystem : MonoBehaviour
{
    public static HapticSystem hapticSystem { get; private set; }

    public bool Haptic = true;


    private void Start()
    {
        if (hapticSystem == null)
            hapticSystem = this;
    }


    public void Vibrate()
    {
        if(Haptic)
        MMVibrationManager.Vibrate();
    }

    public void VibrateLong()
    {
        if (Haptic)
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }

    public void VibrateShort()
    {
        if (Haptic)
            MMVibrationManager.Vibrate();
    }
}
