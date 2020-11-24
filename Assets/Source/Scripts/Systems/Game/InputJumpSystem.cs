using Kuhpik;
using UnityEngine;

public class InputJumpSystem : GameSystem, IUpdating
{
    [SerializeField] int framesBeforeJumpEnds;
    int framesFromTouch;

    void IUpdating.OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            framesFromTouch = 0;
        }

        else if (Input.GetMouseButton(0))
        {
            framesFromTouch++;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //Не прошло достаточно кадров, что бы не считали действие за попытку прыжка
            if (framesFromTouch <= framesBeforeJumpEnds)
            {
                Jump();
            }
        }
    }

    void Jump()
    {

    }
}
