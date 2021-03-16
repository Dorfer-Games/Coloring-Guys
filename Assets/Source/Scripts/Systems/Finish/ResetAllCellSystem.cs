using Kuhpik;
using DG.Tweening;
using UnityEngine;


public class ResetAllCellSystem : GameSystem, IIniting
{


    void IIniting.OnInit()
    {
        DOTween.KillAll();
            for(int b = 0; b < game.cellsList.Length; b++)
            {
                    var cell = game.cellsList[b];
            cell.SetColor(Color.white);
            cell.SetDown(false);
                    cell.Cell.transform.DOLocalMoveY(config.GetValue(EGameValue.CellUpY), 0f);
        }
    }
}
