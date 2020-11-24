using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButtonComponent : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => Bootstrap.GameRestart(0));
    }
}
