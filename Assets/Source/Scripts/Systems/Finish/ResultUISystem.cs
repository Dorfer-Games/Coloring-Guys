using Kuhpik;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResultUISystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{
    void IIniting.OnInit()
    {
        if (game.isVictory) screen.VictoryPanel.SetActive(true);
        else screen.AlmostPanel.SetActive(true);
        player.levelsPlayed++;

        SendAppMetrica();
        screen.StartCorutineButton();
        HapticSystem.hapticSystem.VibrateLong();
        screen.NoThanksButton.onClick.AddListener(() => NoThanksLevelVictory());
    }

    void SendAppMetrica()
    {
        var levelName = game.environment.name.Replace("(Clone)", "").Trim();
        var progress = GetProgress();

        var @params = new Dictionary<string, object>()
        {
            { "level_number", player.level + 1 },
            { "level_name", levelName },
            { "level_count", player.levelsPlayed },
            { "level_diff", game.characters.Length - 1 },
            { "level_loop", game.levelLoop },
            { "level_random", 0},
            { "level_type", game.levelType },
            { "result", game.isVictory ? "win" : "lose" },
            { "time", DateTime.Now.Subtract(game.gameStartTime).TotalSeconds},
            { "progress", progress }
        };

        AppMetrica.Instance.ReportEvent("level_finish", @params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    void NoThanksLevelVictory()
    {
        if (game.isVictory) LevelLoadingSystem.loadingSystem.AddLevel();
        MoneyRewardedSystem.rewardedSystem.AnimationStart(Bootstrap.GetSystem<LiderboardFinishSystem>().moneyNotThanks, MoneyRewardedSystem.rewardedSystem.startPoint_No, () => Bootstrap.GetSystem<AdsSystem>().AdsInterstitialEndLevelGame());
    }

    int GetProgress()
    {
        var toSubstract = game.isVictory ? 0 : 1; //Вычтем нашего игрока из счёта смертей.
        var killed = game.characters.Where(x => x.isDeath).Count() - toSubstract;
        var percent = (float)killed / (float)(game.characters.Length - 1);

        return Mathf.FloorToInt(percent * 100);
    }
}
