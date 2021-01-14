using UnityEngine.UI;

using UnityEngine;

public class UILevelsProgressBar : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject process, passed, notPassed;


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
}
