using UnityEngine;

//Объект должен висеть на пустышке и управлять объектами внутри себя
public class CellComponent : MonoBehaviour
{
    [field: SerializeField] public GameObject Cell { get; private set; } //Основной объект
    [field: SerializeField] public GameObject Trigger { get; private set; } //Триггер. Включается, если реальный объект внизу (нужен для краски)
    [field: SerializeField] public MeshRenderer Renderer { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public bool IsDown { get; private set; }

    //Только обновляем информацию. Красить будем из системы.
    public void SetColor(Color color)
    {
        Color = color;
    }

    //Тоже самое, только информация.
    public void SetState(bool isDown)
    {
        IsDown = isDown;
    }
}
