using UnityEngine;

using DG.Tweening;

public class AnimationMoneyRewarded : MonoBehaviour
{
    [SerializeField] private float durationAnimation;
    [SerializeField] private Transform startPoint, target;

    public float Duration => durationAnimation * 2;

    private void OnEnable()
    {
        transform.position = startPoint.position;

        var AnimationSequence = DOTween.Sequence();
        AnimationSequence.Append(transform.DOScale(new Vector3(1f,1f,1f), durationAnimation));
        AnimationSequence.Append(transform.DOMove(target.position, durationAnimation));
        AnimationSequence.Join(transform.DOScale(new Vector3(0.137f, 0.137f, 0.137f), durationAnimation));
        AnimationSequence.OnComplete(() => Disabled());
        AnimationSequence.SetEase(Ease.Linear);
        AnimationSequence.Play();
    }

    public void SetStartPoint(Transform point)
    {
        startPoint = point;
    }
    private void Disabled()
    {
        gameObject.SetActive(false);
    }
}