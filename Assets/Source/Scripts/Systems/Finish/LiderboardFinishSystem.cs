using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Collections;

public class LiderboardFinishSystem : GameSystemWithScreen<FinishUIScreen>, IIniting
{
    [SerializeField] GameObject[] leaderboardElementFinishPrefab;
    List<LiderboardFinishComponent> InstanceCharactersFinishLiderboard;
    List<string> namesAddLiderboard = new List<string>();
    int moneyFinishLiderboard = 100, mesto = 7;
    CharactersNamingSystem naming;
    [HideInInspector] public int moneyNotThanks;

    void IIniting.OnInit()
    {
        StartCoroutine(timeStartRandomName());
    }

    public void AddDeathPlayer(GameObject player)
    {
        int index = 0;

        for (int b = 0; b < InstanceCharactersFinishLiderboard.Count; b++)
        {
            if (player.name == InstanceCharactersFinishLiderboard[b].Player.name)
            {
                index = b;
                break;
            }
        }

        InstanceCharactersFinishLiderboard[mesto].UpdateName(naming.ListNamesPlayers[index], mesto + 1);
        namesAddLiderboard.Add(naming.ListNamesPlayers[index]);
        mesto--;
    }

    private void RandomMesta()
    {
        for (int b = 0; b < game.characters.Length; b++)
        {
            if (!game.characters[b].isDeath)
            {
                InstanceCharactersFinishLiderboard[mesto].UpdateName(naming.ListNamesPlayers[b], mesto + 1);
                namesAddLiderboard.Add(naming.ListNamesPlayers[b]);
                mesto--;
            }
        }
    }

    private void UpdateMoneyLiderboard()
    {
        for (int b = 0; b < InstanceCharactersFinishLiderboard.Count; b++)
        {
            InstanceCharactersFinishLiderboard[b].UpdateMoney(moneyFinishLiderboard);

            if (InstanceCharactersFinishLiderboard[b].ReturnName() == "You")
            {
                screen.moneyAdText.text = "+" + Convert.ToString(moneyFinishLiderboard * 5);
                screen.moneyNotThanksText.text = "+" + Convert.ToString(moneyFinishLiderboard);
                moneyNotThanks = moneyFinishLiderboard;
                player.money_round = moneyFinishLiderboard * 5;
            }

            if (b == 0) moneyFinishLiderboard -= 20;
            if (b == 1) moneyFinishLiderboard -= 30;
            if (b > 1) moneyFinishLiderboard -= 5;
        }
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

    private IEnumerator timeStartRandomName()
    {
        yield return new WaitForSeconds(0.05f);
        RandomMesta();
        UpdateMoneyLiderboard();
    }
}