using System.Collections.Generic;
using UnityEngine;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    public class GameData
    {
        public GameObject level;
        public Character[] characters;

        public bool isVictory;

        //-----------Helping-------------//
        public Dictionary<Transform, Character> characterDictionary; //������� ������� ��� ����� LINQ ������ �� ����������.
        public Dictionary<Transform, CellComponent> cellDictionary; //GetComponent �� ��� ������, �� ��� �� 5 ����� ��� ����� ���� ����������
        public CellComponent[] cellsList;
    }
}