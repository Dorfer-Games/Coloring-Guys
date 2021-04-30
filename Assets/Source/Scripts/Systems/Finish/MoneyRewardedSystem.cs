using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class MoneyRewardedSystem : GameSystem
{
    public static MoneyRewardedSystem rewardedSystem { get; private set; }
    public Transform startPoint_X5, startPoint_No; // Точки, с которых будут стартовать анимация монеток

    [SerializeField] private GameObject moneyAnimation;
    [SerializeField] private Button Next, NoThinks;

    void Start()
    {
        if (rewardedSystem == null) rewardedSystem = this;
    }
  
    public void AnimationStart(int moneyCount, Transform startPoint)
    {
        moneyAnimation.GetComponent<AnimationMoneyRewarded>().SetStartPoint(startPoint);
        moneyAnimation.SetActive(true);
        StartCoroutine(StartAnimationRewarded(moneyCount));
    }

    void AddMoney(int moneyCount)
    {
        var lastMoney = player.money;
        var sequence = DOTween.Sequence();
        var moneyUI = Bootstrap.GetSystem<MoneyUIComponent>();

        player.money += moneyCount;

        sequence.SetDelay(moneyAnimation.GetComponent<AnimationMoneyRewarded>().AnimationSequence.Duration() - 0.5f);
        sequence.Append(DOVirtual.Float(lastMoney, player.money, 1f, moneyUI.UpdateMoneyFloat));
        sequence.AppendInterval(0.25f);
        sequence.OnComplete(() => Bootstrap.GameRestart(0));
        sequence.Play();
    }

    IEnumerator StartAnimationRewarded(int moneyCount)
    {
        Next.enabled = false;
        NoThinks.enabled = false;
        yield return null;
        AddMoney(moneyCount);
    }
}
