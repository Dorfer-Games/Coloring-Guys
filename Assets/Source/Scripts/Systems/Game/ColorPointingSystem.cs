using Kuhpik;
using Supyrb;
using UnityEngine;

public class ColorPointingSystem : GameSystem, IIniting, IUpdating
{
    [SerializeField] string arrowGOName = "Rotation Point";

    Transform arrowTransform;
    Transform playerColorSpawnPoint;

    void IIniting.OnInit()
    {
        arrowTransform = game.characters[0].rigidbody.transform.Find(arrowGOName);

        Signals.Get<ColorPickedupSignal>().AddListener(OnColorCollected);
        Signals.Get<ColorSpawnedSignal>().AddListener(OnColorSpawned);
    }

    void IUpdating.OnUpdate()
    {
        if (playerColorSpawnPoint != null)
        {
            arrowTransform.LookAt(playerColorSpawnPoint);
        }
    }

    void OnColorSpawned(Transform point, int index)
    {
        if (index == 0) arrowTransform.gameObject.SetActive(true);
        playerColorSpawnPoint = point;
    }

    void OnColorCollected(int index)
    {
        if (index == 0) arrowTransform.gameObject.SetActive(false);
    }
}
