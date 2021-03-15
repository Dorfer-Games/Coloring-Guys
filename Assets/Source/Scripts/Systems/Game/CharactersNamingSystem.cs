using Kuhpik;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class CharactersNamingSystem : GameSystem, IIniting
{
    [SerializeField] private string[] names;
    private string pathNameFile = "", name;
    public List<string> ListNamesPlayers = new List<string>();
    void IIniting.OnInit()
    {
        TextAsset fileText = (TextAsset)Resources.Load("NameCharacters", typeof(TextAsset));
        names = fileText.text.Split();
        game.characters[0].rigidbody.name = "Player";
        ListNamesPlayers.Add("You");
        for (int i = 1; i < game.characters.Length; i++)
        {
            Name();
            game.characters[i].rigidbody.name = name;
        }
    }

    private void Name()
    {
        int randomName = Random.Range(0, names.Length);
        if (names[randomName] != "")
        {
            name = names[randomName];
            ListNamesPlayers.Add(name);
        }
        else Name();
    }
}
