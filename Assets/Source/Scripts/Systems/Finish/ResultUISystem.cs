using Kuhpik;
using System.Collections.Generic;

public class ResultUISystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{
    void IIniting.OnInit()
    {
        if (game.isVictory) screen.VictoryPanel.SetActive(true);
        else screen.AlmostPanel.SetActive(true);

        screen.StartCorutineButton();
        HapticSystem.hapticSystem.VibrateLong();
        screen.NoThanksButton.onClick.AddListener(() => NoThanksLevelVictory());
    }

    void SendAppMetrica()
    {
        var @params = new Dictionary<string, object>()
        {
            { "level", player.level}
        };

        AppMetrica.Instance.ReportEvent("level_finish", @params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    void NoThanksLevelVictory()
    {
        if (game.isVictory) LevelLoadingSystem.loadingSystem.AddLevel();
        MoneyRewardedSystem.rewardedSystem.AnimationStart(Bootstrap.GetSystem<LiderboardFinishSystem>().moneyNotThanks, MoneyRewardedSystem.rewardedSystem.startPoint_No);
        Bootstrap.GetSystem<AdsSystem>().AdsInterstitialEndLevelGame();
    }

    void LevelNotVictory()
    {
        SendAppMetrica();
        Bootstrap.GameRestart(0);
    }
}
