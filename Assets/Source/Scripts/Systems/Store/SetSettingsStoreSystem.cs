using Kuhpik;

using UnityEngine;

public class SetSettingsStoreSystem : GameSystemWithScreen<StoreUI>, IIniting
{
    [SerializeField] private Camera cameraStore, mainCamera;


    void IIniting.OnInit()
    {
        cameraStore.enabled = true;
        mainCamera.enabled = false;
        screen.closeButton.onClick.AddListener(delegate { CloseStore(); });
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
