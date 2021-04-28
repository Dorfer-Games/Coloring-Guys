using Kuhpik;
using UnityEngine;

public class ColorStackComponent : MonoBehaviour
{
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    public Transform Parent { get; private set; }
    public Color Color { get; private set; }
    public int Count { get; private set; }

    GameData data;

    public void Setup(Color color, int count, GameData data)
    {
        this.data = data;
        Color = color;
        Count = count;
    }
    public void UpdatePosition()
    {
        Debug.Log("Update");
        //transform.position = new Vector3(Random.Range(23.85f, 1.29f), transform.position.y, Random.Range(3f, 15f));
        var rng = data.cellsList.GetRandom().transform.position;
        rng.y = transform.position.y;
        transform.position = rng;
    }
  }