using DG.Tweening;
using Kuhpik;
using Supyrb;
using TMPro;
using UnityEngine;

public class GameUIScreen : UIScreen
{
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] TextMeshProUGUI hexCountText;

    public override void Subscribe()
    {
        base.Subscribe();
        Signals.Get<HexCountChangedSignal>().AddListener(UpdateHexCount);
        Signals.Get<PlayerNotificationSignal>().AddListener(ShowNotification);
    }

    void UpdateHexCount(Character character, int value)
    {
        if (character.isPlayer)
        {
            hexCountText.gameObject.SetActive(value > 0);
            hexCountText.text = $"HEX: {value}";
        }
    }

    void ShowNotification(string notification)
    {
        notificationText.gameObject.SetActive(true);
        notificationText.text = notification;

        notificationText.transform.DOPunchScale(Vector3.one * 1.1f, 1.5f, 5, 0.25f).OnComplete(() =>
        {
            notificationText.gameObject.SetActive(false);
        });
    }
}
