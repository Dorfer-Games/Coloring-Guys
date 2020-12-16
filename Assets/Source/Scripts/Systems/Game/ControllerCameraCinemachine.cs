using UnityEngine;

using Cinemachine;
// Скрипт нужно повесить на Cinemachine Камеру
public class ControllerCameraCinemachine : MonoBehaviour
{
    [SerializeField] private Vector3 positionCamera;

    private CinemachineVirtualCamera virtualCamera;
    
    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.transform.position = positionCamera;
    }
    

    public void SetSettingsCamera(Transform targetObject)
    {
        virtualCamera.Follow = targetObject;
        virtualCamera.LookAt = targetObject;
    }
}
