using UnityEngine;

using System.Collections;
using Kuhpik;
using TMPro;
using UnityEngine.UI;

public class MoneyRewardedSystem : GameSystem, IIniting
{
    public static MoneyRewardedSystem rewardedSystem { get; private set; }
    public Transform startPoint_X5, startPoint_No; // Точки, с которых будут стартовать анимация монеток
    [SerializeField] private GameObject moneyAnimation;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private Button Next;
    private void Start()
    {
        if (rewardedSystem == null) rewardedSystem = this;
    }
    void IIniting.OnInit()
    {
        moneyText.text = player.money.ToString();
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
        moneyText.text = player.money.ToString();
    }
    private IEnumerator StartAnimationRewarded(int moneyCount)
    {
        Next.enabled = false;
        yield return new WaitForSeconds(1.7f);
        AddMoney(moneyCount);
        yield return new WaitForSeconds(0.5f);
        Bootstrap.GameRestart(0);
    }
}
