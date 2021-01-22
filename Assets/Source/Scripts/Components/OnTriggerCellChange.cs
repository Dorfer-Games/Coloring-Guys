using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerCellChange : MonoBehaviour
{
    [SerializeField] private CellComponent cellComponent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<ColorStackComponent>(out var component))
        {
            if (cellComponent.IsUp) {
                component.UpdatePosition();
            }
        }
    }
}
