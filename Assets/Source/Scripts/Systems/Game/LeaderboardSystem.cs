using Kuhpik;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderboardSystem : GameSystemWithScreen<GameUIScreen>, IIniting, IUpdating
{
    [SerializeField] GameObject leaderboardElementPrefab;

    Dictionary<Character, LeaderboardUIElement> elementsDict;
    int counter;

    void IIniting.OnInit()
    {
        elementsDict = new Dictionary<Character, LeaderboardUIElement>();

        foreach (var character in game.characters)
        {
            var component = Instantiate(leaderboardElementPrefab, screen.Leaderboard).GetComponent<LeaderboardUIElement>();
            component.UpdateColor(character.color);
            component.UpdateName(character.rigidbody.name);
            elementsDict.Add(character, component);
        }

        UpdatePlaces();
    }

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

        for (int i = 0; i < orderedList.Length; i++)
        {
            var ui = elementsDict[orderedList[i]];
            ui.UpdateName(orderedList[i].rigidbody.name, orderedList[i].isDeath);
            ui.UpdatePlace(i);
            ui.transform.SetAsLastSibling();
        }
    }
}
