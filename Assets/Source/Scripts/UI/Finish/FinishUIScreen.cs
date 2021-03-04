using Kuhpik;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishUIScreen : UIScreen
{
    [field: SerializeField] public GameObject VictoryPanel { get; private set; }
    [field: SerializeField] public GameObject AlmostPanel { get; private set; }
    [field: SerializeField] public Button TryAgainButton { get; private set; }
    [field: SerializeField] public Button NoThanksButton { get; private set; }
    [field: SerializeField] public Button AdsRewardedButton { get; private set; }
    [field: SerializeField] public RateUsComponent RateUsUI { get; private set; }
    [SerializeField] private TMP_Text moneyText;
    private MoneyUIComponent moneyUIComponent;

    private void Start()
    {
        StartCoroutine(EnabledButtonNoThinks());
        moneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        moneyUIComponent.UpdateMoney += (money) => { moneyText.text = money.ToString(); };
    }


    private IEnumerator EnabledButtonNoThinks()
    {
        yield return new WaitForSeconds(3f);
        NoThanksButton.gameObject.SetActive(true);
    }
}
