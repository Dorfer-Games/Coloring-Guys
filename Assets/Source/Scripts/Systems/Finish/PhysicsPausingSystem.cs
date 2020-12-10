using Kuhpik;
using UnityEngine;

public class PhysicsPausingSystem : GameSystem, IIniting, IDisposing
{
    void IIniting.OnInit()
    {
        Physics.autoSimulation = false;
    }

    void IDisposing.OnDispose()
    {
        Physics.autoSimulation = true;
    }
}
