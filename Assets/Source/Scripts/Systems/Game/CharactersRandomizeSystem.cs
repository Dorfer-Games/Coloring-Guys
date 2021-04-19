using Kuhpik;
using System.Linq;
using UnityEngine;

public class CharactersRandomizeSystem : GameSystem, IIniting
{
    [SerializeField] Mesh[] characterMeshes;
    private StoreItem[] storeItems;

    private void Start()
    {
        storeItems = Resources.LoadAll<StoreItem>("Store");
    }

    public int GetIndexOfSkin(StoreItem skin)
    {
        return storeItems.ToList().IndexOf(skin);
    }

    public void UpdateSkins()
    {
        for (int d = 0; d < game.characters.Length; d++)
        {
            if (d == 0)
            {
                game.characters[d].DataAllToPlayer.skin = player.selectedSkinPlayer;
                game.characters[d].DataAllToPlayer.EnabledSkinPlayer();
            }

            else
            {
                game.characters[d].DataAllToPlayer.skin = Random.Range(0, storeItems.Length);
                game.characters[d].DataAllToPlayer.EnabledSkinPlayer();
            }
        }

        Bootstrap.GetSystem<CharactersSpawnSystem>().SetComponentPlayer(game.characters.Length);
    }

    public void UpdateSkinsPlayer()
    {
        game.characters[0].DataAllToPlayer.skin = player.selectedSkinPlayer;
        game.characters[0].DataAllToPlayer.EnabledSkinPlayer();

        Bootstrap.GetSystem<CharactersSpawnSystem>().SetComponentPlayer(1);
    }

    void IIniting.OnInit()
    {
        game.characters[0].DataAllToPlayer.skin = player.selectedSkinPlayer;
        game.characters[0].DataAllToPlayer.EnabledSkinPlayer();

        Bootstrap.GetSystem<CharactersSpawnSystem>().SetComponentPlayer(game.characters.Length);
    }
}
