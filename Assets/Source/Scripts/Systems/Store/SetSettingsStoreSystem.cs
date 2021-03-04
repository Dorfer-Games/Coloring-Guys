using Kuhpik;

using UnityEngine;

using Cinemachine;

public class SetSettingsStoreSystem : GameSystemWithScreen<StoreUI>, IIniting
{
    [SerializeField] private Camera cameraStore, mainCamera;

    private CinemachineVirtualCamera virtualCamera;
    void IIniting.OnInit()
    {
        cameraStore.enabled = true;
        mainCamera.enabled = false;
        screen.closeButton.onClick.AddListener(delegate { CloseStore(); });
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = game.characters[0].rigidbody.transform; // Цель за, которой двигается камера
        virtualCamera.LookAt = game.characters[0].rigidbody.transform; // Цель за, которой следит камера
    }

    public void OpenStore()
    {
        cameraStore.enabled = true;
        mainCamera.enabled = false;
    }
    private void CloseStore()
    {
        cameraStore.enabled = false;
        mainCamera.enabled = true;
        Bootstrap.ChangeGameState(EGamestate.Menu);
    }
}
