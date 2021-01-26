using UnityEngine;

using DG.Tweening;

public class AnimationMoneyRewarded : MonoBehaviour
{
    [SerializeField] private float durationAnimation;

    [SerializeField] private Transform target, startPoint;

    private void Start()
    {
        transform.position = startPoint.position;
        var anim = DOTween.Sequence();
        anim.Append(transform.DOScale(new Vector3(1f,1f,1f), durationAnimation));
        anim.Append(transform.DOMove(target.position, durationAnimation));
        anim.Join(transform.DOScale(new Vector3(0.137f, 0.137f, 0.137f), durationAnimation));
        anim.OnComplete(() => Disabled());
        anim.SetEase(Ease.Linear);
        anim.Play();
    }


    private void Disabled()
    {
        gameObject.SetActive(false);
    }
}