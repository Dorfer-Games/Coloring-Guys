using UnityEngine;

using UnityEngine.UI;
using Kuhpik;
public class SoreItemComponent : MonoBehaviour
{
    public StoreItem storeItem;
    private PurchasedStoreSystem purchasedStoreSystem;
    [Tooltip("Иконка Главного предмета")]
    [SerializeField] Image imageMain;
    [Tooltip("Куплен предмет или нет")]
    [SerializeField] bool purchasedItemStore;
    [SerializeField] private int indexSkin; // какой по счёту скин мы выбрали
    private Sprite ImageMainItem;




    private void Start()
    {
        InitItemStore();
        purchasedStoreSystem = FindObjectOfType<PurchasedStoreSystem>();
    }
    public void InitItemStore()
    {
        ImageMainItem = storeItem.imageMain;
        purchasedItemStore = storeItem.purchasedItemStore;
        indexSkin = storeItem.indexSkin;

        if (purchasedItemStore)
        {
            imageMain.sprite = storeItem.imageMain;
        }
    }


   public void SelectedSkinPlayer()
    {
        if (purchasedItemStore)
            purchasedStoreSystem.SelectedStoreSkinPlayer(indexSkin);
    }
    public void ChangeItem(GameObject item)
    {
        if (item == gameObject)
        {
            imageMain.sprite = ImageMainItem;
            purchasedItemStore = true;
        }
    }
}
