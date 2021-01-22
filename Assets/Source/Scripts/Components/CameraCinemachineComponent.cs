using UnityEngine;

using Cinemachine;
using NaughtyAttributes;
using System.Collections;
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraCinemachineComponent : MonoBehaviour
{
    [SerializeField] private Transform targetObject; // Цель, за которой будет следить камера

    [SerializeField] [BoxGroup("Settings Follow Camera")] Vector3 positionCamera;
    [SerializeField] [BoxGroup("Settings Follow Camera")] float smoothMovement = 0f;


    [SerializeField] [BoxGroup("Settings LookAt Camera")] Vector3 target_rotationCamera;

    private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer cinemachineTransposer;
    private CinemachineComposer cinemachineComposer;

    private void Start()
    {
        // Получаем компоненты
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        cinemachineComposer = virtualCamera.GetCinemachineComponent<CinemachineComposer>();


        #region Set Settings Camera
        //Присваеваем наши настройки для камеры
        cinemachineTransposer.m_FollowOffset = positionCamera;
        cinemachineTransposer.m_YawDamping = smoothMovement;
        cinemachineComposer.m_TrackedObjectOffset = target_rotationCamera;
        #endregion
    }

    private void Update()
    {
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
        //StartCoroutine(SetCameraSetting());
    }

    private IEnumerator SetCameraSetting()
    {
        yield return new WaitForSeconds(3f);
        virtualCamera.LookAt = null;
    }
}
