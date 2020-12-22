using UnityEngine;

using Cinemachine;
using NaughtyAttributes;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraCinemachineComponent : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // Цель, за которой будет следить камера

    [SerializeField] [BoxGroup("Settings Follow Camera")] Vector3 positionCamera;
    [SerializeField] [BoxGroup("Settings Follow Camera")] float smoothMovement = 0f;


    [SerializeField] [BoxGroup("Settings LookAt Camera")] Vector3 target_rotationCamera;

    private CinemachineVirtualCamera virtualCamera;
    
    private void Start()
    {
        // Получаем компоненты
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        CinemachineTransposer cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        CinemachineComposer cinemachineComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();


        #region Set Settings Camera
        //Присваеваем наши настройки для камеры
        cinemachineTransposer.m_FollowOffset = positionCamera;
        cinemachineTransposer.m_YawDamping = smoothMovement;
        cinemachineComposer.m_TrackedObjectOffset = target_rotationCamera;
        #endregion
    }


    public void SetSettingsCamera(Transform targetObject)
    {
        virtualCamera.Follow = targetObject; // Цель за, которой двигается камера
        virtualCamera.LookAt = targetObject; // Цель за, которой следит камера
    }
}
