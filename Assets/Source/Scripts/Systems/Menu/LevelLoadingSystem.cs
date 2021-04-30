using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Supyrb;
using System;
using System.Linq;
using UnityEngine;

public class LevelLoadingSystem : GameSystem, IIniting, IDisposing
{
    //Используем папку ресурсов для оптимизации памяти.
    [Header("Environment")]
    [SerializeField] string environmentPath;
    [SerializeField] int environmentsMax;

    [Header("Hex")]
    [SerializeField] string hexPath;
    [SerializeField] [ReorderableList] string[] hexTypes; //Будем зацикливать

    [Header("Others")]
    public int currentLevel = 0;
    public int countLevelsFirstIteration = 9; // количество уровней в одной итерации. 9, потому что отсчёт идёт с 0
    public System.Action<int> OnLevel;
    public int levelAmount = 0, levelUIProgressBar;

    public static LevelLoadingSystem loadingSystem { get; private set; }

    private void Awake()
    {
        if (loadingSystem == null) loadingSystem = this;
        DOTween.Init().SetCapacity(3200, 200);
    }

    // Надо переписать, это вообще не понятно.
    void IIniting.OnInit()
    {
        #region Loading Levels

        levelAmount = player.level - player.lastIterationLevels;

        for (int d = 0; d <= countLevelsFirstIteration; d++)
        {
            if (levelUIProgressBar <= player.level)
            {
                levelUIProgressBar += 5;
            }

            if (levelUIProgressBar > player.level)
            {
                levelUIProgressBar -= 5;
                player.lastIterationLevels = levelUIProgressBar;
                break;
            }

            if (d >= countLevelsFirstIteration)
            {
                d = 0;
            }
        }

        if (levelAmount > countLevelsFirstIteration)
        {
            if (player.numberIterationLevels < (environmentsMax - 1))
            {
                player.numberIterationLevels++;
            }

            else
            {
                player.numberIterationLevels = 0;
            }

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

        var hexIndex = GameloopExtensions.CalculateLoopIndex(player.level, 5, hexTypes.Length); //Используй это же.
        var environmentIndex = GameloopExtensions.CalculateLoopIndex(player.level, 5, environmentsMax);

        var environment = Resources.Load<GameObject>(string.Format(environmentPath, environmentIndex + 1)); //Потому что уровни начинаются не с 0 а с 1.
        var cells = Resources.Load<GameObject>(string.Format(hexPath, hexTypes[hexIndex]));

        game.environment = Instantiate(environment);
        game.cells = Instantiate(cells);
        game.levelType = hexTypes[hexIndex];
        game.levelLoop = player.numberIterationLevels + 1;

        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
        game.cellsList = FindObjectsOfType<CellComponent>();
    }

    public void AddLevel()
    {
        player.level++;
    }

    void IDisposing.OnDispose()
    {
        Signals.Clear();
        DOTween.KillAll();
    }
}
