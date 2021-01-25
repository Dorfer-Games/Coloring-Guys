using UnityEngine;
using System.Collections.Generic;

//Объект должен висеть на пустышке и управлять объектами внутри себя
public class CellComponent : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private LayerMask groundLayer;

    [field: SerializeField] public GameObject Cell { get; private set; } //Основной объект
    [field: SerializeField] public GameObject Trigger { get; private set; } //Триггер. Включается, если реальный объект внизу (нужен для краски)
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public bool IsUp { get; set; }
    [field: SerializeField] public bool IsGoingToGoUp { get; set; }
    [field: SerializeField] public Character CharacterWhoCollored { get; set; }

    // Красим через метод в тонком монобехе для сохранения принципа DRY.
    // Иначе кастомную логику краски придётся копировать в разных системах. (Сейчас покраска вызывается в 2ух системах)
    public void SetColor(Color color)
    {
        Color = color;

        var block = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(block);
        block.SetVector("_Color", color);
        meshRenderer.SetPropertyBlock(block);
    }

    // Устанавливаем и
    public void SetUp(bool isUp)
    {
        IsUp = isUp;
    }

    public List<CellComponent> GetCellsAround()
    {
        float lengthOfCkeckingRay = 10f;
        float startOffset = 0f;
        int numOfCheckingCells = 6;
        float stepOfAngel = 360f / 6f;
        var cells = new List<CellComponent>();
        for (int i = 0; i < numOfCheckingCells; i++)
        {
            Vector3 addingVector = new Vector3(Mathf.Cos((i * stepOfAngel + startOffset) / 180f * Mathf.PI), 5, Mathf.Sin((i * stepOfAngel + startOffset) / 180f * Mathf.PI));
            Vector3 checkPos = transform.position + addingVector * 2f;
            bool resultOfRay = Physics.Raycast(checkPos, Vector2.down, out var hit, lengthOfCkeckingRay, groundLayer);
            if (resultOfRay)
            {
                cells.Add(hit.collider.transform.parent.GetComponent<CellComponent>());
            }
        }
        return cells;
    }

}
