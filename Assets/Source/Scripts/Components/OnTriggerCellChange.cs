using UnityEngine;

public class OnTriggerCellChange : MonoBehaviour
{
    [SerializeField] private CellComponent cellComponent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Color"))
        {
            if (other.transform.TryGetComponent<ColorStackComponent>(out var component))
            {
                if (cellComponent.IsDown)
                {
                    component.UpdatePosition();
                }
            }
        }
    }
}
