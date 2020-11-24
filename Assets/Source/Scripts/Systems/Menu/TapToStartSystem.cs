using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class TapToStartSystem : GameSystem, IIniting
{
    [SerializeField] Button taptostartButton;

    void IIniting.OnInit()
    {
        taptostartButton.onClick.AddListener(() => Bootstrap.ChangeGameState(EGamestate.Game));
    }
}
