using System;
using UnityEngine;
using System.Collections;

public class OnTriggerEnterImpactComponent: MonoBehaviour
{

    public event Action<Transform, Transform> OnEnter;

    public Transform lastCollisionPlayer;
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
        OnEnter?.Invoke(other.transform, transform);
        VFXCollisionEffects.Play();
        if (toPlayer)
            HapticSystem.hapticSystem.Vibrate();
    }
    public void SetLastPlayer(Transform Object)
    {
        lastCollisionPlayer = Object;
        StartCoroutine(ResetlastPlayer());
    }

    private IEnumerator ResetlastPlayer()
    {
        yield return new WaitForSeconds(1f);
        lastCollisionPlayer = null;
    }
}
