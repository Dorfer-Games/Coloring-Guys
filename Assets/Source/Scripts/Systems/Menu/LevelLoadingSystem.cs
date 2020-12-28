using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;


public class LevelLoadingSystem : GameSystem, IIniting, IDisposing
{
    public static LevelLoadingSystem loadingSystem { get; private set; }
    public GameObject[] levels;
    private int currentLevel = 0;


    public System.Action<int> OnLevel;

    private void Awake()
    {
        if(loadingSystem == null)
        loadingSystem = this;
    }



    void IIniting.OnInit()
    {
        PlayerData data = new PlayerData();
        data.level = levels.Length;
        #region Loading Levels
            if (levels.Length > config.GetValue(EGameValue.LevelsCount))
            {
                CreateLevel((int)config.GetValue(EGameValue.LevelsCount));
            }
            else
            {
                int randomLevel = Random.Range(1, levels.Length);
                CreateLevel(randomLevel);
            }

        #endregion
    }

    private void CreateLevel(int level)
    {
        currentLevel = level + 1;
        OnLevel.Invoke(currentLevel);
        game.level = Instantiate(levels[level]);
        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
    }


    void IDisposing.OnDispose()
    {
        Signals.Clear();
        DOTween.KillAll();
    }
}
