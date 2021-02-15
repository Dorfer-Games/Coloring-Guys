using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishUIScreen : UIScreen
{
    [field: SerializeField] public GameObject VictoryPanel { get; private set; }
    [field: SerializeField] public GameObject AlmostPanel { get; private set; }
    [field: SerializeField] public Button TryAgainButton { get; private set; }
    [field: SerializeField] public Button NextButton { get; private set; }
    [field: SerializeField] public RateUsComponent RateUsUI { get; private set; }
}
