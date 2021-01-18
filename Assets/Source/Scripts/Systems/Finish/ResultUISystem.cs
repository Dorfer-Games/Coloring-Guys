using Kuhpik;

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
        screen.TryAgainButton.onClick.AddListener(() => Bootstrap.GameRestart(0));
        screen.NextButton.onClick.AddListener(() => Level());
    }



    private void Level()
    {
        LevelLoadingSystem.loadingSystem.AddLevel();
        MoneyRewardedSystem.rewardedSystem.AnimationStart();
    }
}
