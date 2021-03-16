using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kuhpik;
public class SpawnStoreItemSystem : GameSystem, IIniting
{
    [SerializeField] private GameObject StoreItemPrefab, parentSpawn;
    [HideInInspector]
    public StoreItem[] storeItems;
    [HideInInspector]
    public List<SoreItemComponent> StoreItem = new List<SoreItemComponent>();
    public void OnInit()
    {
        storeItems = Resources.LoadAll<StoreItem>("Store");
        int b = 0;
        foreach (var items in storeItems)
        {
            var Item = Instantiate(StoreItemPrefab, parentSpawn.transform).GetComponent<SoreItemComponent>();
            Item.storeItem = items;
            Item.indexSpawnSet = b;
            StoreItem.Add(Item);
            b++;
        }
    }
}
