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

    private void Start()
    {
        StartCoroutine(EnabledButtonNoThinks());
    }


    private IEnumerator EnabledButtonNoThinks()
    {
        yield return new WaitForSeconds(3f);
        NoThanksButton.gameObject.SetActive(true);
    }
}
