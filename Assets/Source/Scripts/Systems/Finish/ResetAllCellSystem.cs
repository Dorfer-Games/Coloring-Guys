using Kuhpik;
using DG.Tweening;
using UnityEngine;


public class ResetAllCellSystem : GameSystem, IIniting
{


    void IIniting.OnInit()
    {
        if (game.isVictory)
        {
            for(int b = 0; b < game.cellsList.Length; b++)
            {
                try
                {
                    var cell = game.cellsList[b].Cell;
                    cell.transform.DOLocalMoveY(config.GetValue(EGameValue.CellBaseYPos), 0f);
                }
                catch { }
            }
        }
    }
}
