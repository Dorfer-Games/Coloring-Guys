using System;

using UnityEngine;
using Kuhpik;

public class PurchasedStoreSystem : GameSystemWithScreen<StoreUI>, IIniting
{
    public Action<GameObject> itemPurchesed;
    private SpawnStoreItemSystem SpawnitemSystem;
    [SerializeField] private int priceItemStore = 500;
    private bool findRandomSkin;
    private MoneyUIComponent MoneyUIComponent;

    public void OnInit()
    {
        SpawnitemSystem = Bootstrap.GetSystem<SpawnStoreItemSystem>();
        ChangeMoneyPlayer_PriceSkin();
        MoneyUIComponent = FindObjectOfType<MoneyUIComponent>();
        MoneyUIComponent.UpdateMoney += (money) => { ChangeMoneyPlayer_PriceSkin(); };
        foreach (var items in SpawnitemSystem.StoreItem)
        {
             itemPurchesed += items.ChangeItem;
        }
    }
    private void ChangeMoneyPlayer_PriceSkin()
    {
        if (player.money < priceItemStore || player.countOpensItemStore > SpawnitemSystem.StoreItem.Count)
            screen.purhased.gameObject.SetActive(false);
        if (player.money >= priceItemStore && player.countOpensItemStore < SpawnitemSystem.StoreItem.Count)
            screen.purhased.gameObject.SetActive(true);
    }

    public void SelectedStoreSkinPlayer(int indexSkin)
    {
                player.selectedSkinPlayer = indexSkin;
        for (int b = 0; b < SpawnitemSystem.StoreItem.Count; b++)
        {
            if(indexSkin == b)
            SpawnitemSystem.StoreItem[b].Selected(true);
            else SpawnitemSystem.StoreItem[b].Selected(false);
        }
        Bootstrap.GetSystem<CharactersRandomizeSystem>().UpdateSkinsPlayer();
        Bootstrap.GetSystem<ColorStackDisplaySystem>().Init();
    }
    public void PurchasedItem()
    {
        if (player.money >= priceItemStore && player.countOpensItemStore < SpawnitemSystem.StoreItem.Count) {
            for(int b = 0; b < SpawnitemSystem.StoreItem.Count; b++) {
                findRandomSkin = true;
                int randomItem = RandomSkinOpenStore();
                findRandomSkin = false;
                var item = SpawnitemSystem.StoreItem[randomItem];
                if (!SpawnitemSystem.storeItems[randomItem].purchasedItemStore) {
                    itemPurchesed?.Invoke(item.gameObject);
                    player.countOpensItemStore++;
                    SpawnitemSystem.storeItems[randomItem].purchasedItemStore = true;
                    SpawnitemSystem.storeItems[randomItem].Save();
                    player.money -= priceItemStore;
                    Bootstrap.GetSystem<MoneyUIComponent>().UpdateMoney.Invoke(player.money);
                    break;
                }
            }
        }
    }


    private int RandomSkinOpenStore()
    {
        while (true)
        {
            int randomItem = UnityEngine.Random.Range(0, SpawnitemSystem.StoreItem.Count);
            if (!SpawnitemSystem.storeItems[randomItem].purchasedItemStore)
            {
                return randomItem;
            }
        }
    }
}
