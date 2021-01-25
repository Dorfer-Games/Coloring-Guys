using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuUIScreen : UIScreen
{
    [field: SerializeField] public Button TapToRestartButton;
    [field: SerializeField] public Transform LevelsProgressBar;
    

    
    public override void Subscribe()
    {
        base.Subscribe();
        TapToRestartButton.onClick.AddListener(() => Bootstrap.ChangeGameState(EGamestate.Game));
    }
}
