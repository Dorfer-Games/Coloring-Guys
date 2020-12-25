using UnityEngine;
using System;


public interface ImpulseTrap
{
    void ImpulseTrap(Transform other);
}

public interface JumpTrap
{
    void JumpTrap(Transform other);
}

public class TrapsBehaviour : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;



    public virtual void MovementTrap()
    {

    }


    public void UpdateColor(Color colorTrap)
    {
        for (int b = 0; b < meshRenderers.Length; b++)
        {
            var block = new MaterialPropertyBlock();
            meshRenderers[b].GetPropertyBlock(block);
            block.SetVector("_Color", colorTrap);
            meshRenderers[b].SetPropertyBlock(block);
            print("Set");
        }
    }
}