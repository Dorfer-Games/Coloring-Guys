using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LiderboardFinishSystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{
    [SerializeField] GameObject[] leaderboardElementFinishPrefab;
    List<LiderboardFinishComponent> InstanceCharactersFinishLiderboard;
    int moneyFinishLiderboard = 25, mesto = 7;
    CharactersNamingSystem naming;

    void IIniting.OnInit()
    {

    }


    public void AddDeathPlayer(GameObject player)
    {
        try
        {
            InstanceCharactersFinishLiderboard[mesto].UpdateName(naming.ListNamesPlayers[mesto], mesto + 1, moneyFinishLiderboard);
            if (mesto >= 7 && mesto >= 3)
                moneyFinishLiderboard += 5;
            if (mesto == 2)
                moneyFinishLiderboard = 50;
            if (mesto == 1)
                moneyFinishLiderboard = 80;
            if (mesto == 0)
                moneyFinishLiderboard = 100;
            mesto--;
        }
        catch { }
    }
    public void InitLiderbordFinish()
    {
        InstanceCharactersFinishLiderboard = new List<LiderboardFinishComponent>();
        naming = Bootstrap.GetSystem<CharactersNamingSystem>();
        for (int b = 0; b < game.characters.Length; b++)
        {
            try
            {
                var Instance = Instantiate(leaderboardElementFinishPrefab[b], screen.Liderboard.transform).GetComponent<LiderboardFinishComponent>();
                Instance.Player = game.characters[b].rigidbody.gameObject;
                InstanceCharactersFinishLiderboard.Add(Instance);
            }
            catch
            {
                break;
            }
        }
    }
}