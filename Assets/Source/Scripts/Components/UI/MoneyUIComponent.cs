using TMPro;
using Kuhpik;
using UnityEngine;
using System;
public class MoneyUIComponent : GameSystem, IIniting
{
    public Action<int> UpdateMoney;

   
    void IIniting.OnInit()
    {
        UpdateMoney.Invoke(player.money);
    }
}
