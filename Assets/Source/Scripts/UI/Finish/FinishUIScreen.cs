using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishUIScreen : UIScreen
{
    [field: SerializeField] public TextMeshProUGUI ResultText { get; private set; }
    [field: SerializeField] public TextMeshProUGUI ButtonText { get; private set; }
    [field: SerializeField] public Button RestartButton { get; private set; }
}
