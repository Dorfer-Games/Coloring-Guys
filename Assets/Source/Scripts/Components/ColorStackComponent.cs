using UnityEngine;

public class ColorStackComponent : MonoBehaviour
{
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    public Transform Parent { get; private set; }
    public Color Color { get; private set; }
    public int Count { get; private set; }

    private void Start()
    {
        ChangeRaycast();
    }

    private void ChangeRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up * 10f);
        Debug.DrawRay(transform.position, -transform.up * 10f, Color.red);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag != "Cell")
            {
                UpdatePosition();
            }
        }
    }
    public void Setup(Transform parent, Color color, int count)
    {
        Renderer.material.color = color;
        if (parent != null) Parent = parent;
        Color = color;
        Count = count;
    }


    private void UpdatePosition()
    {
            transform.position = new Vector3(0f, 0f, 0f);
        ChangeRaycast();
        }
    }