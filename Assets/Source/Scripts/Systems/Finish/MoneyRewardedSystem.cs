using UnityEngine;

using System.Collections;
using Kuhpik;
using TMPro;
using UnityEngine.UI;

public class MoneyRewardedSystem : GameSystem
{
    public static MoneyRewardedSystem rewardedSystem { get; private set; }
    public Transform startPoint_X5, startPoint_No; // Точки, с которых будут стартовать анимация монеток
    [SerializeField] private GameObject moneyAnimation;
    [SerializeField] private Button Next, NoThinks;
    private void Start()
    {
        if (rewardedSystem == null) rewardedSystem = this;
    }
  
    public void AnimationStart(int moneyCount, Transform startPoint)
    {
        moneyAnimation.GetComponent<AnimationMoneyRewarded>().SetStartPoint(startPoint);
        moneyAnimation.SetActive(true);
        StartCoroutine(StartAnimationRewarded(moneyCount));
    }


    private void AddMoney(int moneyCount)
    {
        player.money += moneyCount;
        Bootstrap.GetSystem<MoneyUIComponent>().UpdateMoney.Invoke(player.money);
    }
    private IEnumerator StartAnimationRewarded(int moneyCount)
    {
        Next.enabled = false;
        NoThinks.enabled = false;
        yield return new WaitForSeconds(1.7f);
        AddMoney(moneyCount);
        yield return new WaitForSeconds(0.5f);
        Bootstrap.GameRestart(0);
    }
}
