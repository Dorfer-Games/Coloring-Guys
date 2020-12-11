using Kuhpik;
using System.Linq;
using UnityEngine;

public class DebugCheatingSystem : GameSystem, IIniting, IDisposing
{
    [SerializeField] GameObject[] cheatingPanels;

    void IIniting.OnInit()
    {
        config.Init(config.GameValusConfigs);

#if DEBUG
        foreach (var panel in cheatingPanels)
        {
            panel.SetActive(true);
            panel.transform.GetComponentsInChildren<CheatSliderComponent>().Any(x => x.Subscribe(config.gameValuesDict[x.Type]));
        }
#endif
    }

    //Можно было бы это сделать в каком-то левом классе, тогда можно было бы не делать ресет.
    //Сейчас проблема в том, что я не могу поменять создать копию GameConfig и заменить его во всех системах.
    void IDisposing.OnDispose()
    {
#if UNITY_EDITOR

#endif
    }
}
