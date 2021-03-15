using Kuhpik;

using UnityEngine;

using Cinemachine;

public class SettingsCamera : GameSystem, IIniting
{
    [SerializeField] private Camera camera, mainCamera;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    public void UpdateDataCamera()
    {
        virtualCamera.Follow = game.characters[0].rigidbody.transform; // Цель за, которой двигается камера
        virtualCamera.LookAt = game.characters[0].rigidbody.transform; // Цель за, которой следит камера
    }

    private void Open()
    {
        virtualCamera.gameObject.SetActive(true);
        camera.enabled = true;
        mainCamera.enabled = false;
    }

    public void OnInit()
    {
        Open();
    }
}
