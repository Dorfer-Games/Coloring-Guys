using Kuhpik;
using System.Collections.Generic;
using UnityEngine;
public class ResultUISystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{

 

    void IIniting.OnInit()
    {    
        if (game.isVictory) {
            screen.VictoryPanel.SetActive(true);
        }
        else
        {
            screen.AlmostPanel.SetActive(true);
        }
        screen.StartCorutineButton();

        Bootstrap.GetSystem<AdsSystem>().AdsInterstitialEndLevelGame();
      
        HapticSystem.hapticSystem.VibrateLong();
        screen.NoThanksButton.onClick.AddListener(() => NoThanksLevelVictory());
    }

    private void SendAppMetrica()
    {
        var @params = new Dictionary<string, object>()
        {
            { "level", player.level}
        };

        AppMetrica.Instance.ReportEvent("level_finish", @params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    private void NoThanksLevelVictory()
    {
        if (game.isVictory)
            LevelLoadingSystem.loadingSystem.AddLevel();
        MoneyRewardedSystem.rewardedSystem.AnimationStart(100, MoneyRewardedSystem.rewardedSystem.startPoint_No);
    }

    private void LevelNotVictory()
    {
        SendAppMetrica();
        Bootstrap.GameRestart(0);
    }
}
