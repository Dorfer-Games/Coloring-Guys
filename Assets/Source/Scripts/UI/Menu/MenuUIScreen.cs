using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIScreen : UIScreen
{
    [field: SerializeField] public Button TapToRestartButton;

    public override void Subscribe()
    {
        base.Subscribe();
        TapToRestartButton.onClick.AddListener(() => Bootstrap.ChangeGameState(EGamestate.Game));
    }
}
