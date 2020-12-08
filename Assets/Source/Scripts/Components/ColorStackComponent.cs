using UnityEngine;

public class ColorStackComponent : MonoBehaviour
{
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    public Color Color { get; private set; }
    public int Count { get; private set; } 

    public void Setup(Color color, int count)
    {
        Renderer.material.color = color;
        Color = color;
        Count = count;
    }
}