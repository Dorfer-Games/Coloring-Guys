using UnityEngine;

public class ColorStakRaycastComponent : MonoBehaviour
{
  

    private void UpdatePosition()
    {
        transform.position = new Vector3(Random.Range(1.3f, 33f), transform.position.y, Random.Range(2.1f, 25f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other)
        {
            UpdatePosition();
        }
    }
}