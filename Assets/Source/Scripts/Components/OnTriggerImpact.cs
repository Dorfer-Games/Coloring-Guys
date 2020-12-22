using UnityEngine;

public class OnTriggerImpact : MonoBehaviour
{
    [SerializeField] private bool thisImpactObject; // true, если нужно отбрасывать этого character(а)

    [SerializeField] private Transform player;
    private OnTriggerEnterImpactComponent impactComponent;

    private void Start()
    {
        impactComponent = GetComponentInParent<OnTriggerEnterImpactComponent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (thisImpactObject)
            {
                impactComponent.TriggerEnterImact(player);
            }
            else
            {
                impactComponent.TriggerEnterImact(other.transform);
            }
        }
    }
}
