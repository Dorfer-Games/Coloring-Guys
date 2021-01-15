using UnityEngine;

using System.Collections;
using Kuhpik;
using TMPro;

public class MoneyRewardedSystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{
    public static MoneyRewardedSystem rewardedSystem { get; private set; }

    [SerializeField] private GameObject moneyAnimation;
    [SerializeField] private TMP_Text moneyText;
    private void Start()
    {
        if (rewardedSystem == null) rewardedSystem = this;
    }
    void IIniting.OnInit()
    {
        moneyText.text = player.money.ToString();
    }
    public void AnimationStart()
    {
        moneyAnimation.SetActive(true);
        StartCoroutine(StartAnimationRewarded());
    }


    private void AddMoney(int moneyCount)
    {
        player.money += 100;
        moneyText.text = player.money.ToString();
    }
    private IEnumerator StartAnimationRewarded()
    {
        yield return new WaitForSeconds(1.7f);
        AddMoney(100);
        moneyAnimation.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Bootstrap.GameRestart(0);
    }
}
