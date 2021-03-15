using TMPro;

using UnityEngine;

public class LiderboardFinishComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText, moneyText, NumberText;


    public GameObject Player;
    public void UpdateName(string name, int mestoLiderboard)
    {
        nameText.text = name;

            NumberText.text = mestoLiderboard.ToString();
    }


    public void UpdateMoney(int money)
    {
        moneyText.text = money.ToString();
    }
    public string ReturnName()
    {
        return nameText.text;
    }
}
