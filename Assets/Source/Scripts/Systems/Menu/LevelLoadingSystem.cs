using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;


public class LevelLoadingSystem : GameSystem, IIniting, IDisposing
{
    public static LevelLoadingSystem loadingSystem { get; private set; }
    public GameObject[] levels;
    public int currentLevel = 0;


    public System.Action<int> OnLevel;

    private void Awake()
    {
        if(loadingSystem == null)
        loadingSystem = this;
    }



    void IIniting.OnInit()
    {
        #region Loading Levels
            if (levels.Length > player.level)
            {
                CreateLevel(player.level);
            }
            else
            {
                int randomLevel = Random.Range(0, levels.Length);
                CreateLevel(randomLevel);
            }

        #endregion
    }

    private void CreateLevel(int level)
    {
        currentLevel = level;
        OnLevel?.Invoke(player.level);
        game.level = Instantiate(levels[level]);
        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
        game.cellsList = FindObjectsOfType<CellComponent>();
    }

    public void AddLevel()
    {
        if (player.level <= levels.Length)
        {
            player.level++;
        }
    }


    void IDisposing.OnDispose()
    {
        Signals.Clear();
        DOTween.KillAll();
    }
}
