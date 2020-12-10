using Kuhpik;

public class ResultUISystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{
    void IIniting.OnInit()
    {
        screen.ResultText.text = game.isVictory ? "Victory!" : "Almost";
        screen.ButtonText.text = game.isVictory ? "Next level" : "Try again";

        screen.RestartButton.onClick.AddListener(() => Bootstrap.GameRestart(0));
    }
}
