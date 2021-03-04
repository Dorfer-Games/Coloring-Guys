using Kuhpik;

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreUI : UIScreen
{
    public TMP_Text PurchasedTextPrice;
    [SerializeField] private TMP_Text MoneyText;
    public Button closeButton, AdsRewarded;

    private MoneyUIComponent moneyUIComponent;

    void Start()
    {
        moneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        moneyUIComponent.UpdateMoney += (money) => { MoneyText.text = money.ToString(); };
    }

}
