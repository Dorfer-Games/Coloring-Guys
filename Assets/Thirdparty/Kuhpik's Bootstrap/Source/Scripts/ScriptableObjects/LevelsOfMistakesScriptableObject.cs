using System.Collections;
using UnityEngine;
using System;


[Serializable]
public struct LevelOfMistakes
{
    public int levelOfMistake;
    public float mistakeDelay;
}

[CreateAssetMenu(menuName = "Game / MistakeLevels")]
public class LevelsOfMistakesScriptableObject : ScriptableObject
{
    [SerializeField] public LevelOfMistakes[] levelsOfMistakes;

    public float GetMistakeDelay(int levelOfMistake)
    {
        for (int i = 0; i < levelsOfMistakes.Length; i++)
        {
            if(levelsOfMistakes[i].levelOfMistake == levelOfMistake)
            {
                return levelsOfMistakes[i].mistakeDelay;
            }
        }
        return Mathf.Infinity;
    }
}