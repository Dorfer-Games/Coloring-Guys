using System;

using UnityEngine;
using Kuhpik;


public class PurchasedStoreSystem : GameSystemWithScreen<StoreUI>, IIniting
{
    public Action<GameObject> itemPurchesed;
    private SpawnStoreItemSystem SpawnitemSystem;
    [SerializeField] private int priceItemStore = 500;
    private bool findRandomSkin;
    public void OnInit()
    {
        screen.MoneyText.text = player.money.ToString();
        SpawnitemSystem = Bootstrap.GetSystem<SpawnStoreItemSystem>();
        screen.PurchasedTextPrice.text = priceItemStore.ToString();
        foreach (var items in SpawnitemSystem.SoreItem)
        {
             itemPurchesed += items.ChangeItem;
        }
    }


    public void PurchasedItem()
    {
        if (player.money >= priceItemStore && player.countOpensItemStore < SpawnitemSystem.SoreItem.Count) {
            for(int b = 0; b < SpawnitemSystem.SoreItem.Count; b++) {
                findRandomSkin = true;
                int randomItem = RandomSkinOpenStore();
                findRandomSkin = false;
                var item = SpawnitemSystem.SoreItem[randomItem];
                if (!SpawnitemSystem.storeItems[randomItem].purchasedItemStore) {
                    itemPurchesed?.Invoke(item.gameObject);
                    player.countOpensItemStore++;
                    SpawnitemSystem.storeItems[randomItem].purchasedItemStore = true;
                    SpawnitemSystem.storeItems[randomItem].Save();
                    break;
                }
            }
        }
    }


    private int RandomSkinOpenStore()
    {
        while (true)
        {
            int randomItem = UnityEngine.Random.Range(0, SpawnitemSystem.SoreItem.Count);
            if (!SpawnitemSystem.storeItems[randomItem].purchasedItemStore)
            {
                return randomItem;
            }
        }
    }
}
