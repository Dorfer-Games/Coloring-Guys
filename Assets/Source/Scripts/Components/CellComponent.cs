using UnityEngine;

//Объект должен висеть на пустышке и управлять объектами внутри себя
public class CellComponent : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;

    [field: SerializeField] public GameObject Cell { get; private set; } //Основной объект
    [field: SerializeField] public GameObject Trigger { get; private set; } //Триггер. Включается, если реальный объект внизу (нужен для краски)
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public bool IsDown { get; private set; }

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
    public void SetDown(bool isDown)
    {
        IsDown = isDown;
    }
}
