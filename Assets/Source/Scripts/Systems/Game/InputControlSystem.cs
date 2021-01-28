using Kuhpik;
using UnityEngine;

public class InputControlSystem : GameSystem, IUpdating, IIniting
{
    [SerializeField] float maxDelta;
    [SerializeField] Joystick joystick;
    Vector2 touchPosition;

    void IIniting.OnInit()
    {
        joystick = FindObjectOfType<Joystick>();
    }

    void IUpdating.OnUpdate()
    {
        if (Mathf.Abs(joystick.Vertical) > 0.1f || Mathf.Abs(joystick.Horizontal) > 0.1f)
            game.playerRotation = new Vector3(0, (Mathf.Atan2(joystick.Vertical, -joystick.Horizontal) * 180 / Mathf.PI) - 90, 0);


        #region Old Input
        /*if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }

        else if (Input.GetMouseButton(0))
        {
            var mouse = Input.mousePosition;
            var delta = mouse.x - touchPosition.x;
            game.characters[0].rotationValue = Mathf.Clamp(delta / maxDelta, -1, 1);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            game.characters[0].rotationValue = 0;
        }*/
        #endregion
    }
}
