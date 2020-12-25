using DG.Tweening;
using Kuhpik;
using Supyrb;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameUIScreen : UIScreen
{
    [SerializeField] TextMeshProUGUI notificationText;
    [SerializeField] TextMeshProUGUI hexCountText;

    [field: SerializeField] public RectTransform Leaderboard { get; private set; }

    bool canDisplayNotification = true;

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
            hexCountText.text = value.ToString();
        }
    }

    async void ShowNotification(string notification)
    {
        if (canDisplayNotification)
        {
            notificationText.gameObject.SetActive(true);
            notificationText.text = notification;
            canDisplayNotification = false;

            notificationText.transform.DOPunchScale(Vector3.one * 1.1f, 1.5f, 5, 0.25f).OnComplete(() =>
            {
                notificationText.gameObject.SetActive(false);
            });

            await Task.Delay(TimeSpan.FromSeconds(2f));

            canDisplayNotification = true;
        }
    }
}
