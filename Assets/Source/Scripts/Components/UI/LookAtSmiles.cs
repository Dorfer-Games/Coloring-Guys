using UnityEngine;

using UnityEngine.UI;

public class LookAtSmiles : MonoBehaviour
{
    public Transform Target;
    public int smilesType;
    [SerializeField] private Sprite[] SmilesJoy, SmilesAngry;
    [SerializeField] private Image Element;
    [SerializeField] private float timeDestroy;


    private void Start()
    {
        Destroy(gameObject, timeDestroy);

        SetSmilesTypes();
    }

    private void SetSmilesTypes()
    {
        if (smilesType == 0)
            Element.sprite = SmilesJoy[Random.Range(0, SmilesJoy.Length)];

        if (smilesType == 1)
            Element.sprite = SmilesAngry[Random.Range(0, SmilesAngry.Length)];
    }

    private void Update()
    {
        UpdateSmilesCoordinates();
    }


    private void UpdateSmilesCoordinates()
    {
        Vector3 targetPosition = Target.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPosition);
        if (Mathf.Approximately(screenPos.z, 0))
        {
            return;
        }

        Vector3 halfScreen = new Vector3(Screen.width, Screen.height) / 2;

        Vector3 screenPosNoZ = screenPos;
        screenPosNoZ.z = 0;

        Vector3 screenCenterPos = screenPosNoZ - halfScreen;


        if (screenPos.z < 0)
        {
            screenCenterPos *= -1;
        }




        if (screenPos.z < 0 || screenPos.x > Screen.width || screenPos.x < 0 ||
            screenPos.y > Screen.height || screenPos.y < 0)
        {

            Element.gameObject.SetActive(false);

            transform.rotation =
                Quaternion.FromToRotation(Vector3.up, screenCenterPos);


            Vector3 norSCP = screenCenterPos.normalized;


            if (norSCP.x == 0)
            {
                norSCP.x = 0.01f;
            }
            if (norSCP.y == 0)
            {
                norSCP.y = 0.01f;
            }


            Vector3 xScreenCP = norSCP * (halfScreen.x / Mathf.Abs(norSCP.x));

            Vector3 yScreenCP = norSCP * (halfScreen.y / Mathf.Abs(norSCP.y));


            if (xScreenCP.sqrMagnitude < yScreenCP.sqrMagnitude)
            {
                screenPos = halfScreen + xScreenCP;
            }
            else
            {
                screenPos = halfScreen + yScreenCP;
            }
        }
        else
        {

            Element.gameObject.SetActive(true);

        }


        float margin = 70;

        screenPos.z = 0;

        screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
        screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);


        transform.position = screenPos;
    }
}
