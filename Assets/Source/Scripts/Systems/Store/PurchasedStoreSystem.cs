using System;

using UnityEngine;
using Kuhpik;
using UnityEngine.UI;
public class PurchasedStoreSystem : GameSystemWithScreen<StoreUI>, IIniting, IUpdating
{
    public Action<GameObject> itemPurchesed;
    private SpawnStoreItemSystem SpawnitemSystem;
    [SerializeField] private int priceItemStore = 500;
    private bool findRandomSkin, animationPurchasedAutoStore;
    private MoneyUIComponent MoneyUIComponent;
    #region Animation Purchased Auto Store
    [Header("Animation Purchased Auto Store")]
    [SerializeField] private float time = 3f;
    [SerializeField] private float offset = 0.1f;
    private float Timer;
    #endregion




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
        {
            screen.purchasedImage.sprite = screen.SpriteDeactivateButtonPirchased;
            screen.purhased.enabled = false;
        }
        if (player.money >= priceItemStore && player.countOpensItemStore < SpawnitemSystem.StoreItem.Count)
        {
            screen.purchasedImage.sprite = screen.SpriteActivateButtonPirchased;
            screen.purhased.enabled = true;
        }
    }

    public void SelectedStoreSkinPlayer(int indexSkin, int indexSpawnSet)
    {
                player.selectedSkinPlayer = indexSkin;
        for (int b = 0; b < SpawnitemSystem.StoreItem.Count; b++)
        {
            if (indexSpawnSet == b)
            {
                SpawnitemSystem.StoreItem[indexSpawnSet].Selected(true);
            }
            else SpawnitemSystem.StoreItem[b].Selected(false);
        }
        Bootstrap.GetSystem<CharactersRandomizeSystem>().UpdateSkinsPlayer();
        Bootstrap.GetSystem<ColorStackDisplaySystem>().Init();
    }



    public void SelectedStoreSkinPlayerAuto()
    {
        HapticSystem.hapticSystem.VibrateShort();
        int random = RandomSkinOpenStore();
        SpawnitemSystem.StoreItem[random].Selected(true);
        for (int b = 0; b < SpawnitemSystem.StoreItem.Count; b++)
        {
            if(b != random)
                    SpawnitemSystem.StoreItem[b].Selected(false);
            }
        }


    public void PurchasedItem()
    {
        Timer = time - offset;
        animationPurchasedAutoStore = true;
    }



    private void Purchased()
    {
        if (player.money >= priceItemStore && player.countOpensItemStore < SpawnitemSystem.StoreItem.Count)
        {
            for (int b = 0; b < SpawnitemSystem.StoreItem.Count; b++)
            {
                findRandomSkin = true;
                int randomItem = RandomSkinOpenStore();
                findRandomSkin = false;
                var item = SpawnitemSystem.StoreItem[randomItem];
                if (!SpawnitemSystem.storeItems[randomItem].purchasedItemStore)
                {
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

    public void OnUpdate()
    {
        if (animationPurchasedAutoStore)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
                if (time < Timer)
                {
                    SelectedStoreSkinPlayerAuto();
                    offset += 0.03f;
                    Timer = time - offset;
                }
            }
            else
            {
                Purchased();
                offset = 0.1f;
                time = 3f;
                animationPurchasedAutoStore = false;
            }
        }
    }
}
