using UnityEngine;

public class LookAtLeaderboardUIElements : MonoBehaviour
{
    public Transform target;

    [SerializeField] private GameObject[] Elements;

    void Update()
    {
        UpdateCordinatesIndicator();
        //LookAtTarget();
    }



    private void UpdateCordinatesIndicator()
    {
        Vector3 targetPosition = target.position;
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

            Elements[0].SetActive(true);

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

            Elements[0].SetActive(false);

        }


        float margin = 70;

        screenPos.z = 0;

        screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
        screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);


        transform.position = screenPos;
    }


    private void LookAtTarget()
    {
        Vector2 direction = target.transform.position - Elements[0].transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Elements[0].transform.rotation = Quaternion.Slerp(Elements[0].transform.rotation, rotation, 3f * Time.deltaTime);
    }
}
