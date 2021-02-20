using UnityEngine;

using UnityEngine.UI;
[CreateAssetMenu(menuName = "Store/StoreItem", fileName = "StoreItem")]
public class StoreItem : ScriptableObject
{
    [Tooltip("Иконка Главного предмета")]
    public Sprite imageMain;
    [Tooltip("Куплен предмет или нет")]
    public bool purchasedItemStore;
}
