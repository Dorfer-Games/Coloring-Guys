using Kuhpik;
using System.Linq;

public class LevelLoadingSystem : GameSystem, IIniting
{
    void IIniting.OnInit()
    {
        game.cellDictionary = FindObjectsOfType<CellComponent>().ToDictionary(x => x.transform, x => x);
    }
}
