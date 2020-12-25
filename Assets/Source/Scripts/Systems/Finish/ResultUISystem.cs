using Kuhpik;

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

        screen.TryAgainButton.onClick.AddListener(() => Bootstrap.GameRestart(0));
        screen.NextButton.onClick.AddListener(() => Bootstrap.GameRestart(0));
    }
}
