using UnityEngine.UI;
using UnityEngine;

public class MenuSettingsComponent : MonoBehaviour
{
    [SerializeField] private Image hapticUI;
    [SerializeField] private Image soundUI;
    [SerializeField] private GameObject SettingMenu;
    [SerializeField] private Sprite hapticOn, hapticOff, soundOn, soundOff;
    [SerializeField] private AudioListener audioListener;
    private bool sound = true, haptic = true;


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
            audioListener.enabled = true;
        }
        else
        {
            soundUI.sprite = soundOff;
            audioListener.enabled = false;
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
