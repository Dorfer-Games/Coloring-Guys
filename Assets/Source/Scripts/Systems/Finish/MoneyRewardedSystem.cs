using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using System;

public class MoneyRewardedSystem : GameSystem
{
    public static MoneyRewardedSystem rewardedSystem { get; private set; }
    public Transform startPoint_X5, startPoint_No; // Точки, с которых будут стартовать анимация монеток

    [SerializeField] private GameObject moneyAnimation;
    [SerializeField] private Button Next, NoThinks;

    Action callback;

    void Start()
    {
        if (rewardedSystem == null) rewardedSystem = this;
    }
  
    public void AnimationStart(int moneyCount, Transform startPoint, Action callback)
    {
        UIManager.GetUIScreen<FinishUIScreen>().AdsRewardedButton.transform.parent.gameObject.SetActive(false);
        moneyAnimation.GetComponent<AnimationMoneyRewarded>().SetStartPoint(startPoint);
        moneyAnimation.SetActive(true);
        this.callback = callback;
        AddMoney(moneyCount);
    }

    void AddMoney(int moneyCount)
    {
        var lastMoney = player.money;
        var sequence = DOTween.Sequence();
        var moneyUI = Bootstrap.GetSystem<MoneyUIComponent>();

        player.money += moneyCount;

        sequence.SetDelay(moneyAnimation.GetComponent<AnimationMoneyRewarded>().Duration - 0.5f);
        sequence.Append(DOVirtual.Float(lastMoney, player.money, 1f, moneyUI.UpdateMoneyFloat));
        sequence.AppendInterval(0.25f);
        sequence.OnComplete(InvokeCallback);
        sequence.Play();
    }

    void InvokeCallback()
    {
        callback?.Invoke();
    }
}
