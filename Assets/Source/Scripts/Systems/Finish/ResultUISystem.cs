using Kuhpik;
using System.Collections.Generic;
using UnityEngine;
public class ResultUISystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{

    GameValueConfig Config;

    void IIniting.OnInit()
    {
        config.Init(config.GameValusConfigs);
        
        if (game.isVictory) {
            screen.VictoryPanel.SetActive(true);
        }
        else
        {
            screen.AlmostPanel.SetActive(true);
        }

        Bootstrap.GetSystem<AdsSystem>().AdsInterstitialEndLevelGame();
        for (int i = 0; i < game.characters.Length; i++)
        {
            game.characters[i].animator.SetBool("idle", true);
        }
        HapticSystem.hapticSystem.VibrateLong();
        screen.TryAgainButton.onClick.AddListener(() => LevelNotVictory());
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
        LevelLoadingSystem.loadingSystem.AddLevel();
        MoneyRewardedSystem.rewardedSystem.AnimationStart(100, MoneyRewardedSystem.rewardedSystem.startPoint_No);
    }

    private void LevelNotVictory()
    {
        SendAppMetrica();
        Bootstrap.GameRestart(0);
    }
}
