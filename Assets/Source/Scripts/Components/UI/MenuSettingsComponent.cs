using UnityEngine.UI;
using UnityEngine;

public class MenuSettingsComponent : MonoBehaviour
{
    [SerializeField] private Image hapticUI;
    [SerializeField] private Image soundUI;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private Sprite hapticOn, hapticOff, soundOn, soundOff;
    [SerializeField] private AudioListener audioListener;
    private static bool sound = true, haptic = true;

    private void Start()
    {
        ChangeSettings();
    }


    private void ChangeSettings()
    {
        if (!haptic)
        {
            hapticUI.sprite = hapticOff;
            HapticSystem.hapticSystem.Haptic = haptic;
        }


        if (!sound)
        {
            soundUI.sprite = soundOff;
            AudioListener.volume = 0f;
        }
    }


    public void Haptic()
    {
        haptic = !haptic;
        HapticSystem.hapticSystem.Haptic = haptic;
        if (haptic)
        {
            hapticUI.sprite = hapticOn;
        }
        else
        {
            hapticUI.sprite = hapticOff;
        }
    }

    public void Sound()
    {
        sound = !sound;
        if (sound)
        {
            soundUI.sprite = soundOn;
            AudioListener.volume = 1.0f;
        }
        else
        {
            soundUI.sprite = soundOff;
            AudioListener.volume = 0f;
        }
    }

    public void Close()
    {
        SettingMenu.SetActive(false);
    }

    public void EnabledMenu()
    {
        SettingMenu.SetActive(true);
    }
}
