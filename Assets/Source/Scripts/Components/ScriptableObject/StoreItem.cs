using UnityEngine;

using UnityEngine.UI;
using System;
using System.IO;

[CreateAssetMenu(menuName = "Store/StoreItem", fileName = "StoreItem")]
public class StoreItem : ScriptableObject
{
    [Tooltip("Обязательно для заполнения!")]
    [SerializeField] private string nameSO;
    public int indexSkin;
    [Tooltip("Иконка Главного предмета")]
    public Sprite imageMain;
    [Tooltip("Куплен предмет или нет")]
    public bool purchasedItemStore;


    private void Awake()
    {
        Load();
    }
    public void Save()
    {
        Data data = new Data();
        data.purchasedItemStore = this.purchasedItemStore;
        string json = JsonUtility.ToJson(data);
        if (File.Exists(Application.persistentDataPath + nameSO + ".txt") == false)
        {
            StreamWriter f = new StreamWriter(Application.persistentDataPath + nameSO + ".txt");
            f.WriteLine(json);
            f.Close();
        }
        else
        {
            File.Delete(Application.persistentDataPath + nameSO + ".txt");
            StreamWriter f = new StreamWriter(Application.persistentDataPath + nameSO + ".txt");
            f.WriteLine(json);
            f.Close();
        }
    }

    private void Load()
    {
        if (File.Exists(Application.persistentDataPath + nameSO + ".txt") == true)
        {
            StreamReader f = new StreamReader(Application.persistentDataPath + nameSO + ".txt");
            Data data = new Data();
            
            string json = f.ReadToEnd();
            data = JsonUtility.FromJson<Data>(json);
            purchasedItemStore = data.purchasedItemStore;
            f.Close();
        }
        else
        {
            Debug.Log("File not find");
        }
    }
}

[Serializable]
class Data
{

    public Sprite imageMain;
    public bool purchasedItemStore;
}
