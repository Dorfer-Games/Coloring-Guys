using Kuhpik;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishUIScreen : UIScreen
{
    [field: SerializeField] public GameObject VictoryPanel { get; private set; }
    [field: SerializeField] public GameObject AlmostPanel { get; private set; }
    [field: SerializeField] public Button NoThanksButton { get; private set; }
    [field: SerializeField] public Button AdsRewardedButton { get; private set; }
    public Transform Liderboard;
    public TMP_Text moneyText, moneyAdText;

    private MoneyUIComponent MoneyUIComponent;
    void Start()
    {
        MoneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        MoneyUIComponent.UpdateMoney += (money) => { moneyText.text = money.ToString(); };
    }
       

    public void StartCorutineButton()
    {
        StartCoroutine(EnabledButtonNoThinks());
    }
    public IEnumerator EnabledButtonNoThinks()
    {
        yield return new WaitForSeconds(3f);
        NoThanksButton.gameObject.SetActive(true);
    }
}
