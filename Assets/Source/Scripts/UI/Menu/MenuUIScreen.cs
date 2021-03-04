using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuUIScreen : UIScreen
{
    [field: SerializeField] public Button TapToRestartButton;
    [field: SerializeField] public Transform LevelsProgressBar;
    [field: SerializeField] public Button AdsRewardedStackColor;
    [SerializeField] private TMP_Text moneyText;


    
 

    public override void Subscribe()
    {
        base.Subscribe();
        TapToRestartButton.onClick.AddListener(() => Bootstrap.ChangeGameState(EGamestate.Game));
    }

    public void Store()
    {
        Bootstrap.ChangeGameState(EGamestate.Store);
    }

    void Start()
    {
        Bootstrap.GetSystem<MoneyUIComponent>().UpdateMoney += (money) => { moneyText.text = money.ToString(); };
    }
}
