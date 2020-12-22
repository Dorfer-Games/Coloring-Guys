using UnityEngine;

public class LookAtLeaderboardUIElements : MonoBehaviour
{
    public Transform target;

    [SerializeField] private GameObject[] Elements;

    void Update()
    {
        // get the position on screen, in screen coordinates
        Vector3 targetPosition = target.position;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(targetPosition);
        if (Mathf.Approximately(screenPos.z, 0))
        {
            return;
        }

        // save half screen resulution because we will need it often
        Vector3 halfScreen = new Vector3(Screen.width, Screen.height) / 2;

        // we don't want the Z-Value in our center-vector because it would
        // cause problems when normalizing it
        Vector3 screenPosNoZ = screenPos;
        screenPosNoZ.z = 0;
        // get the vector from the center of the screen to the
        // calculated screen position
        Vector3 screenCenterPos = screenPosNoZ - halfScreen;

        // we have to invert the vector when we are looking away from the target
        // the vector is just projected on the view-plane, think looking in a mirror
        if (screenPos.z < 0)
        {
            screenCenterPos *= -1;
        }

        // debug check, if the ray is pointing in the wanted direction
        // can only be seen with gizmos enabled, in scene view (3D Mode only)
        //Debug.DrawRay(halfScreen, screenCenterPos.normalized * 100000, Color.red);

        // check if the target is on screen
        if (screenPos.z < 0 || screenPos.x > Screen.width || screenPos.x < 0 ||
            screenPos.y > Screen.height || screenPos.y < 0)
        {
            // if you have a arrow on your symbol, pointing in the
            // direction, enable it here:
            Elements[0].SetActive(true);


            // rotate it to point towards the target position
            transform.rotation =
                Quaternion.FromToRotation(Vector3.up, screenCenterPos);

            // normalized ScreenCenterPosition
            Vector3 norSCP = screenCenterPos.normalized;

            // avoid dividing by zero
            if (norSCP.x == 0)
            {
                norSCP.x = 0.01f;
            }
            if (norSCP.y == 0)
            {
                norSCP.y = 0.01f;
            }

            // stretch the normalized screenCenterPosition so that X is at the edge
            Vector3 xScreenCP = norSCP * (halfScreen.x / Mathf.Abs(norSCP.x));
            // stretch the normalized screenCenterPosition so that Y is at the edge
            Vector3 yScreenCP = norSCP * (halfScreen.y / Mathf.Abs(norSCP.y));

            // compare the streched vectors in length and use the smaller one
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
            // if you have a arrow on your symbol, pointing in the
            // direction, disable it here:
            Elements[0].SetActive(false);

        }

        // clamp the result, so we can always see the full marker/tracker image
        float margin = 70;

        screenPos.z = 0;

        screenPos.x = Mathf.Clamp(screenPos.x, margin, Screen.width - margin);
        screenPos.y = Mathf.Clamp(screenPos.y, margin, Screen.height - margin);

        // set the transform position
        transform.position = screenPos;
    }
}
