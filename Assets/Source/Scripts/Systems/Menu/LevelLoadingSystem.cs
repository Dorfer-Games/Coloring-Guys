using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLoadingSystem : GameSystem, IIniting, IDisposing
{
    public static LevelLoadingSystem loadingSystem { get; private set; }
    public GameObject[] levels;
    public int currentLevel = 0;

    public int countLevelsFirstIteration = 9; // ���������� ������� � ����� ��������. 9, ������ ��� ������ ��� � 0

    public System.Action<int> OnLevel;

    public int levelAmount = 0;



    private void Awake()
    {
        if(loadingSystem == null)
        loadingSystem = this;
    }



    void IIniting.OnInit()
    {
        #region Loading Levels
        levelAmount = player.level - player.lastIterationLevels;
        
        if (levelAmount > countLevelsFirstIteration)
            {
            if (player.numberIterationLevels < (levels.Length - 1))
            {
                player.numberIterationLevels++;
            }
            else //if (player.numberIterationLevels > (levels.Length - 1))

            {
                player.numberIterationLevels = 0;
            }
print(player.numberIterationLevels);
            player.lastIterationLevels = player.level;
            levelAmount = player.level - player.lastIterationLevels;
            CreateLevel(player.numberIterationLevels);
        }
            else
            {
            CreateLevel(player.numberIterationLevels);
        }

        #endregion
    }

    private void CreateLevel(int level)
    {
        OnLevel?.Invoke(player.level);
        //SendAppMetrica();
        game.level = Instantiate(levels[level]);
        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
        game.cellsList = FindObjectsOfType<CellComponent>();
    }

    public void AddLevel()
    {
            player.level++;
    }


    private void SendAppMetrica()
    {
        var @params = new Dictionary<string, object>()
        {
            { "level", player.level + 1 }
        };

        AppMetrica.Instance.ReportEvent("level_start", @params);
        AppMetrica.Instance.SendEventsBuffer();
    }
    void IDisposing.OnDispose()
    {
        Signals.Clear();
        DOTween.KillAll();
    }
}
