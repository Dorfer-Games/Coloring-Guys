using Kuhpik;

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreUI : UIScreen
{
    public TMP_Text PurchasedTextPrice;
    public TMP_Text MoneyText;
    public Button closeButton, AdsRewarded;

    public void MoneyAdd(int money)
    {
        MoneyText.text = money.ToString();
    }

}
