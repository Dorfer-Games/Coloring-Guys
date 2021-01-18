using UnityEngine;

public class SkinedPlayerComponent : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    public void UpdateBodyColor(Color color)
    {
        meshRenderer.materials[1].color = color;
    }
}
