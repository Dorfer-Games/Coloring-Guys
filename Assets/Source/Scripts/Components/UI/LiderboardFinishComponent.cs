﻿using TMPro;

using UnityEngine;

public class LiderboardFinishComponent : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText, moneyText, NumberText;
    public void UpdateName(string name, int mestoLiderboard, int money)
    {
        nameText.text = name;
        NumberText.text = mestoLiderboard.ToString();
        moneyText.text = money.ToString();
    }
}