using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class MoveBoxComponent : MonoBehaviour
{
    [SerializeField] private Transform box;
    [SerializeField] [BoxGroup("Settings Move Box")] float timeSwitchMove;
    [SerializeField] [BoxGroup("Settings Move Box")] float speedMove;
    private bool switchMove = false;



    private void Start()
    {
        StartCoroutine(SwitchMove());
    }




    private void Update()
    {
        
        if (switchMove) // true - движение вправо
        {
            box.transform.Rotate(0f,0f, -speedMove);
        }
        else
        {
            box.transform.Rotate(0f, 0f, speedMove); 
        }
    }


    private IEnumerator SwitchMove()
    {
            switchMove = true;
            yield return new WaitForSeconds(timeSwitchMove);
            switchMove = false;
            yield return new WaitForSeconds(timeSwitchMove);
            StartCoroutine(SwitchMove());
    }
}
