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
    [SerializeField] private GameObject IndicatorStore; // UI индикатор сигнализирующий, что у игрока хватает денег на покупку скина
    private MoneyUIComponent MoneyUIComponent;
    private PurchasedStoreSystem PurchasedStoreSystem;



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
        MoneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        PurchasedStoreSystem = FindObjectOfType<PurchasedStoreSystem>();
        MoneyUIComponent.UpdateMoney += (money) => {
            moneyText.text = money.ToString();
            if(money >= PurchasedStoreSystem.priceItemStore && MoneyUIComponent.playerItemOpenStore != 0)
            {
                IndicatorStore.SetActive(true);
            }
            else if(money < PurchasedStoreSystem.priceItemStore || MoneyUIComponent.playerItemOpenStore == 0)
            {
                IndicatorStore.SetActive(false);
            }
        };
    }
}
