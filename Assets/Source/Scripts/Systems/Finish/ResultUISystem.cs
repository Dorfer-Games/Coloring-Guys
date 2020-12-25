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

        screen.TryAgainButton.onClick.AddListener(() => Bootstrap.GameRestart(0));
        screen.NextButton.onClick.AddListener(() => Level(config.gameValuesDict[EGameValue.LevelsCount]));
    }



    private void Level(GameValueConfig game)
    {
        if (config.GetValue(EGameValue.LevelsCount) <= LevelLoadingSystem.loadingSystem.levels.Length) {
            Config = game;
            Config.value++;
        }
        Bootstrap.GameRestart(0);
    }
}
