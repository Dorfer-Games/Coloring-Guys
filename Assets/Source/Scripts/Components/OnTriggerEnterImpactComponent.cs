using System;
using UnityEngine;

public class OnTriggerEnterImpactComponent: MonoBehaviour
{

    public event Action<Transform, Transform> OnEnter;

    [SerializeField] private ParticleSystem VFXCollisionEffects;
    private bool toPlayer;

    private void Start()
    {
        if (transform.name == "Player")
        {
            toPlayer = true;
        }
    }

    public void TriggerEnterImact(Transform other)
    {
        if(toPlayer)
        OnEnter?.Invoke(other.transform, transform);
        VFXCollisionEffects.Play();
    }
}
