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

        public string levelType; //��� ������
        public int levelLoop;
        public bool isVictory;
        public DateTime gameStartTime;

        //-----------Helping-------------//
        public Dictionary<Transform, Character> characterDictionary; //������� ������� ��� ����� LINQ ������ �� ����������.
        public Dictionary<Transform, CellComponent> cellDictionary; //GetComponent �� ��� ������, �� ��� �� 5 ����� ��� ����� ���� ����������
        public CellComponent[] cellsList;
    }
}