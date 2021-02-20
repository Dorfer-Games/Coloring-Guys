using UnityEngine;

using UnityEngine.UI;

public class SoreItemComponent : MonoBehaviour
{
    [Tooltip("Иконка Главного предмета")]
    [SerializeField] Image imageMain;
    [Tooltip("Куплен предмет или нет")]
    [SerializeField] bool purchasedItemStore;
    [SerializeField] private GameObject ObjectPrice, ObjectPurchesed;

    private Sprite ImageMainItem;

    public void InitItemStore(Sprite item, bool purchased)
    {
        ImageMainItem = item;
        purchasedItemStore = purchased;

        if (purchasedItemStore)
        {
            imageMain.sprite = item;
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
