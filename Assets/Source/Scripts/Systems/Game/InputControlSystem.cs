using Kuhpik;
using UnityEngine;

public class InputControlSystem : GameSystem, IUpdating, IIniting
{
    [SerializeField] float maxDelta;
    Vector2 touchPosition;

    private Joystick joystick;
    void IIniting.OnInit()
    {
        joystick = GameObject.FindObjectOfType<Joystick>();
    }

    void IUpdating.OnUpdate()
    {
        if (Mathf.Abs(joystick.Vertical) > 0.1f || Mathf.Abs(joystick.Horizontal) > 0.1f)
        game.playerRotation = new Vector3(0, (Mathf.Atan2(joystick.Vertical, -joystick.Horizontal) * 180 / Mathf.PI) - 90, 0);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.V))
        {
            game.isVictory = true;
            Bootstrap.ChangeGameState(EGamestate.VFX);
        }
#endif
    }
}
