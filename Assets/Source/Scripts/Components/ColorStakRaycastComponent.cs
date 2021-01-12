using UnityEngine;

public class ColorStakRaycastComponent : MonoBehaviour
{
  

    private void UpdatePosition()
    {
        Debug.Log("Update");
        transform.position = new Vector3(Random.Range(-23.85f, -1.29f), transform.position.y, Random.Range(-3f, -15f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Trigger")
        {
            UpdatePosition();
        }
    }
}