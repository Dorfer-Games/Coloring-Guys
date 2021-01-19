﻿using TMPro;
using Kuhpik;
using UnityEngine;

public class MoneyUIComponent : GameSystem, IIniting
{
    [SerializeField] private TMP_Text moneyText;

    void IIniting.OnInit()
    {
        moneyText.text = player.money.ToString();
    }
}