using System;
using UnityEngine;

public class OnTriggerEnterImpactComponent: MonoBehaviour
{

    public event Action<Transform> OnEnter;

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
        OnEnter?.Invoke(other.transform);
        VFXCollisionEffects.Play();
    }
}
