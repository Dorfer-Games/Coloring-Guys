using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Linq;
using UnityEngine;

public class LevelLoadingSystem : GameSystem, IIniting, IDisposing
{
    [SerializeField] GameObject[] levels;

    void IIniting.OnInit()
    {
        game.level = Instantiate(levels[player.level]);
        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
    }

    void IDisposing.OnDispose()
    {
        Signals.Clear();
        DOTween.KillAll();
    }
}
