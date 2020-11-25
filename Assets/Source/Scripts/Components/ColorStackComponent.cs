using UnityEngine;

public class ColorStackComponent : MonoBehaviour
{
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    public Color Color => Renderer.material.color;
    public int Count { get; private set; } 

    public void Setup(int count)
    {
        Count = count;
    }
}