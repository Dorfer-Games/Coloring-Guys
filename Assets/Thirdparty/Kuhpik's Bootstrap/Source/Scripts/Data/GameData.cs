using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    public class GameData
    {
        public Character[] characters;

        //-----------Helping-------------//
        public Dictionary<Transform, Character> characterDictionary; //Намного быстрее чем через LINQ искать по трансформу.
        public Dictionary<Transform, CellComponent> cellDictionary; //GetComponent не так грузит, но вот на 5 ботов это может быть проблемнее
    }
}