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
        MMVibrationManager.Vibrate();
    }

    public void VibrateLong()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }

    public void VibrateShort()
    {
        MMVibrationManager.Vibrate();
    }
}
