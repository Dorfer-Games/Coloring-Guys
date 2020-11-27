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
        public Dictionary<Transform, Character> characterDictionary; //������� ������� ��� ����� LINQ ������ �� ����������.
        public Dictionary<Transform, CellComponent> cellDictionary; //GetComponent �� ��� ������, �� ��� �� 5 ����� ��� ����� ���� ����������
    }
}