using UnityEngine;

public class ColorStackComponent : MonoBehaviour
{
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    public Transform Parent { get; private set; }
    public Color Color { get; private set; }
    public int Count { get; private set; }

    public void Setup(Transform parent, Color color, int count)
    {
        Renderer.materials[0].color = color;
        if (parent != null) Parent = parent;
        Color = color;
        Count = count;
    }
  }