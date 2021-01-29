using Kuhpik;
using System.Collections.Generic;

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

        for (int i = 0; i < game.characters.Length; i++)
        {
            game.characters[i].animator.SetBool("idle", true);
        }
        HapticSystem.hapticSystem.VibrateLong();
        screen.TryAgainButton.onClick.AddListener(() => LevelNotVictory());
        screen.NextButton.onClick.AddListener(() => LevelVictory());
    }

    private void SendAppMetrica()
    {
        var @params = new Dictionary<string, object>()
        {
            { "level", player.level + 1 }
        };

        AppMetrica.Instance.ReportEvent("level_finish", @params);
        AppMetrica.Instance.SendEventsBuffer();
    }

    private void LevelVictory()
    {
        LevelLoadingSystem.loadingSystem.AddLevel();
        MoneyRewardedSystem.rewardedSystem.AnimationStart();
    }
    private void LevelNotVictory()
    {
        SendAppMetrica();
        Bootstrap.GameRestart(0);
    }
}
