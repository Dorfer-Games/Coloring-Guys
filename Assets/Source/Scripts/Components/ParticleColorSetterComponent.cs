using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColorSetterComponent : MonoBehaviour
{
    [SerializeField] private ColorComponent colorComponent;

    private void Start()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        particleSystem.startColor = colorComponent.color;
    }
}
