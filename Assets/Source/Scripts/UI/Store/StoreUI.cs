using Kuhpik;

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreUI : UIScreen
{
    [SerializeField] private TMP_Text MoneyText;
    public Button closeButton, AdsRewarded, purhased;
    public Sprite SpriteDeactivateButtonPirchased, SpriteActivateButtonPirchased;
    public Image purchasedImage;
    private MoneyUIComponent MoneyUIComponent;

    void Start()
    {
        MoneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        MoneyUIComponent.UpdateMoney += (money) => { MoneyText.text = money.ToString(); };
    }
}
