using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardUIElement : MonoBehaviour
{
    [SerializeField] Image backgroundImage;
    [SerializeField] float colorAlpha;
    [SerializeField] TextMeshProUGUI placeText;
    [SerializeField] TextMeshProUGUI nameText;

    public void UpdateColor(Color color)
    {
        color.a = colorAlpha;
        backgroundImage.color = color;
    }

    /// <summary>
    /// Index zero will be a #1
    /// </summary>
    public void UpdatePlace(int place)
    {
        placeText.text = $"#{place + 1}";
    }

    /// <summary>
    /// 
    /// </summary>
    public void UpdateName(string name, bool isDead = false)
    {
        nameText.text = $"{name}{(isDead ? " (RIP)" : "")}";
    }
}
