using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CameraFollowSystem : GameSystem, IIniting
{
    [SerializeField] float animationTime;
    [SerializeField] Ease ease;
    [SerializeField] Transform mainCamera;
    void IIniting.OnInit()
    {
        Bootstrap.GetSystem<SettingsCamera>().UpdateDataCamera();
        var seq = DOTween.Sequence();
        var camera = mainCamera.transform;
        var camPoint = game.characters[0].rigidbody.transform.Find("Camera point");

        

        seq.Append(camera.DOLocalMove(Vector3.zero, animationTime));
        seq.Join(camera.DOLocalRotate(Vector3.zero, animationTime));
        seq.SetEase(ease);
        seq.Play();
    }
}
