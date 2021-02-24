using UnityEngine;

using UnityEngine.UI;

public class SoreItemComponent : MonoBehaviour
{
    public StoreItem storeItem;
    [Tooltip("Иконка Главного предмета")]
    [SerializeField] Image imageMain;
    [Tooltip("Куплен предмет или нет")]
    [SerializeField] bool purchasedItemStore;
    [SerializeField] private GameObject ObjectPrice, ObjectPurchesed;

    private Sprite ImageMainItem;




    private void Start()
    {
        InitItemStore();
    }
    public void InitItemStore()
    {
        ImageMainItem = storeItem.imageMain;
        purchasedItemStore = storeItem.purchasedItemStore;

        if (purchasedItemStore)
        {
            imageMain.sprite = storeItem.imageMain;
        }
    }


    public void ChangeItem(GameObject item)
    {
        if (item == gameObject)
        {
            imageMain.sprite = ImageMainItem;
        }
    }
}
