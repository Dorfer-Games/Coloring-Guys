using Kuhpik;

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreUI : UIScreen
{
    public TMP_Text PurchasedTextPrice;
    [SerializeField] private TMP_Text MoneyText;
    public Button closeButton, AdsRewarded, purhased;

    private MoneyUIComponent MoneyUIComponent;

    void Start()
    {
        MoneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        MoneyUIComponent.UpdateMoney += (money) => { MoneyText.text = money.ToString(); };
    }
}
