using TMPro;
using Kuhpik;
using UnityEngine;
using System;
public class MoneyUIComponent : GameSystem, IIniting
{
    public Action<int> UpdateMoney;
    [HideInInspector]
    public int playerItemOpenStore;
   
    void IIniting.OnInit()
    {
        ChangeItemStore_Menu();
        UpdateMoney.Invoke(player.money);
    }

    public void UpdateMoneyFloat(float value)
    {
        UpdateMoney.Invoke(Mathf.FloorToInt(value));
    }

    void ChangeItemStore_Menu() // нужно для индикатора в меню(решил так сделать, потому что нужен быстрый результат для того, чтобы отправить apk. Позже сделаю правильнее)
    {
        int count = 0;
        StoreItem[] storeItems = Resources.LoadAll<StoreItem>("Store");
        for (int b = 0; b < storeItems.Length; b++)
        {
            if (storeItems[b].purchasedItemStore)
            {
                count++;
            }
        }
        playerItemOpenStore = storeItems.Length - count;
    }
}
