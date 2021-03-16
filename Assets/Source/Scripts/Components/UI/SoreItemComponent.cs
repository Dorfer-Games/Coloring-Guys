using UnityEngine;

using UnityEngine.UI;
using Kuhpik;
public class SoreItemComponent : MonoBehaviour
{
    public StoreItem storeItem;
    private PurchasedStoreSystem purchasedStoreSystem;
    [Tooltip("Иконка Главного предмета")]
    [SerializeField] Image imageMain, ImageSkin, ImageNotSkin;
    [Tooltip("Куплен предмет или нет")]
    [SerializeField] bool purchasedItemStore;
    [SerializeField] private int indexSkin; // какой по счёту скин мы выбрали
    public int indexSpawnSet; // идекс присваивает система спавна предметов в магазине для иконки выделения скинов в магазине
    [SerializeField] private Sprite ImageMainActiveItem, ImageMainNotActiveItem, ImageSkinItem;




    private void Start()
    {
        InitItemStore();
        purchasedStoreSystem = FindObjectOfType<PurchasedStoreSystem>();
    }
    public void InitItemStore()
    {
        ImageSkinItem = storeItem.imageMain;
        purchasedItemStore = storeItem.purchasedItemStore;
        indexSkin = storeItem.indexSkin;

        if (purchasedItemStore)
        {
            ImageSkin.sprite = storeItem.imageMain;
            ImageNotSkin.gameObject.SetActive(false);
            ImageSkin.gameObject.SetActive(true);
        }
        else
        {
            ImageSkin.gameObject.SetActive(false);
            ImageNotSkin.gameObject.SetActive(true);
        }
        }


   public void SelectedSkinPlayer()
    {
        if (purchasedItemStore)
            purchasedStoreSystem.SelectedStoreSkinPlayer(indexSkin, indexSpawnSet);
    }

    public void Selected(bool selected)
    {
        if (selected) imageMain.sprite = ImageMainActiveItem;
        else imageMain.sprite = ImageMainNotActiveItem;
    }
    public void ChangeItem(GameObject item)
    {
        if (item == gameObject)
        {
            ImageSkin.sprite = ImageSkinItem;
            ImageSkin.gameObject.SetActive(true);
            ImageNotSkin.gameObject.SetActive(false);
            purchasedItemStore = true;
            SelectedSkinPlayer();
        }
    }
}
