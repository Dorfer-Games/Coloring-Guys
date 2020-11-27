using DG.Tweening;
using Kuhpik;
using Supyrb;
using System.Linq;

public class LevelLoadingSystem : GameSystem, IIniting, IDisposing
{
    void IIniting.OnInit()
    {
        Signals.Clear();
        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
    }

    void IDisposing.OnDispose()
    {
        DOTween.KillAll();
    }
}
