using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CameraFollowSystem : GameSystem, IIniting
{
    [SerializeField] float animationTime;
    [SerializeField] Ease ease;

    void IIniting.OnInit()
    {
        var seq = DOTween.Sequence();
        var camera = Camera.main.transform;
        var camPoint = game.characters[0].rigidbody.transform.Find("Camera point");

        camera.SetParent(camPoint, true);

        seq.Append(camera.DOLocalMove(Vector3.zero, animationTime));
        seq.Join(camera.DOLocalRotate(Vector3.zero, animationTime));
        seq.SetEase(ease);
        seq.Play();
    }
}
