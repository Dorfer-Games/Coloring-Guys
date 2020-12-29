using System;
using UnityEngine;


public class OnCollisionEnterComponent : MonoBehaviour
{
    public event Action<Transform, Transform> OnEnter;


    private void Start()
    {
        GameManager.gameManager.StartGame += (startGame) => { if (startGame) GetComponent<Rigidbody>().isKinematic = false; };
    }
    void OnCollisionEnter(Collision collision)
    {
        OnEnter?.Invoke(collision.transform, transform);
    }
}
