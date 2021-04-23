using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    public class GameData
    {
        public GameObject cells;
        public GameObject environment;
        public Character[] characters;
        public Vector3 playerRotation;
        public List<GameObject> Player;

        public string levelType; //»м€ гексов
        public int levelLoop;
        public bool isVictory;
        public DateTime gameStartTime;

        //-----------Helping-------------//
        public Dictionary<Transform, Character> characterDictionary; //Ќамного быстрее чем через LINQ искать по трансформу.
        public Dictionary<Transform, CellComponent> cellDictionary; //GetComponent не так грузит, но вот на 5 ботов это может быть проблемнее
        public CellComponent[] cellsList;
    }
}