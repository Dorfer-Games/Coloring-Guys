using Kuhpik;
using UnityEngine;

public class PhysicsPausingSystem : GameSystem, IIniting, IDisposing
{
    void IIniting.OnInit()
    {
        Physics.autoSimulation = false;
        for (int i = 0; i < game.characters.Length; i++)
        {
            game.characters[i].animator.SetBool("idle", true);
        }
    }

    void IDisposing.OnDispose()
    {
        Physics.autoSimulation = true;
    }
}
