using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

public class LeaderboardSystem : GameSystemWithScreen<GameUIScreen>, IIniting, IUpdating
{
    [SerializeField] GameObject leaderboardElementPrefab;
    [SerializeField] GameObject leaderboardElementFinishPrefab, screenFinish;
    Dictionary<Character, LeaderboardUIElement> elementsDict;
    List<string> nameCharactersFinishLiderboard = new List<string>();
    int counter, moneyFinishLiderboard = 100;
    public event Action<String> OnCrownEnter;
    void IIniting.OnInit()
    {
        InitLiderbordGame();
        InitLiderbordFinish();
    }

    private void InitLiderbordGame()
    {
        elementsDict = new Dictionary<Character, LeaderboardUIElement>();

        foreach (var character in game.characters)
        {
            var component = Instantiate(leaderboardElementPrefab, screen.Leaderboard).GetComponent<LeaderboardUIElement>();
            component.UpdateColor(character.color);
            component.UpdateName(character.rigidbody.name);
            component.Target = character.rigidbody.transform;
            elementsDict.Add(character, component);
        }

        UpdatePlaces();
    }

    private void InitLiderbordFinish()
    {
        var namingSystem = Bootstrap.GetSystem<CharactersNamingSystem>();
        for (int b = 0; b < game.characters.Length; b++)
        {
            var Instance = Instantiate(leaderboardElementFinishPrefab, screenFinish.transform).GetComponent<LiderboardFinishComponent>();
            NamePlayersFinishLiderboard(Instance, namingSystem, b + 1);
            moneyFinishLiderboard -= 5;
        }
    }

    private void NamePlayersFinishLiderboard(LiderboardFinishComponent liderboardFinish, CharactersNamingSystem namingSystem, int mestoPlayerLiderboard)
    {
        int randomNameINT = UnityEngine.Random.Range(0, namingSystem.names.Length);
        string name = namingSystem.names[randomNameINT];
        if (!nameCharactersFinishLiderboard.Contains(name))
        {
            if (mestoPlayerLiderboard == 1) name = "Player";
            liderboardFinish.UpdateName(name, mestoPlayerLiderboard, moneyFinishLiderboard);
            nameCharactersFinishLiderboard.Add(name);
        }
        else NamePlayersFinishLiderboard(liderboardFinish, namingSystem, mestoPlayerLiderboard);
    } // Подбирает имена

    void IUpdating.OnUpdate()
    {
        counter++;
        //Пропускаем 6 кадров между проверками лидера во имя оптимизации
        if (counter >= 6)
        {
            counter = 0;
            UpdatePlaces();
        }
    }

    void UpdatePlaces()
    {
        foreach (var character in game.characters)
        {
            if (character.isDeath)
            {
                character.colored = 0;
            }

            else
            {
                character.colored = game.cellDictionary.Values.Count(x => x.Color == character.color);
            }
        }

        var orderedList = game.characters.OrderByDescending(x => x.colored).ToArray();
        OnCrownEnter?.Invoke(orderedList[0].rigidbody.name);
        for (int i = 0; i < orderedList.Length; i++)
        {
            var ui = elementsDict[orderedList[i]];
            ui.UpdateName(orderedList[i].rigidbody.name, orderedList[i].isDeath);
            //ui.UpdatePlace(i);
            //ui.transform.SetAsLastSibling();
        }
    }
}
