using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UILevelsProgressBar : MonoBehaviour
{
    [SerializeField] private GameObject process, passed, notPassed;
    [SerializeField] private TMP_Text numberText;


    public void NotPassed()
    {
        notPassed.SetActive(true);
        passed.SetActive(false);
        process.SetActive(false);
    }

    public void Passed()
    {
        passed.SetActive(true);
        notPassed.SetActive(false);
        process.SetActive(false);
    }

    public void Process()
    {
        process.SetActive(true);
        notPassed.SetActive(false);
        passed.SetActive(false);
    }
    public void SetText(int levelCount)
    {
        numberText.text = levelCount.ToString();
    }
}
