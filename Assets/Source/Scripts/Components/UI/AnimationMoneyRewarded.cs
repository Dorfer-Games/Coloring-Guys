using UnityEngine;

using DG.Tweening;

public class AnimationMoneyRewarded : MonoBehaviour
{
    [SerializeField] private float durationAnimation;
    [SerializeField] private Transform startPoint, target;

    public Sequence AnimationSequence { get; private set; }

    private void Start()
    {
        transform.position = startPoint.position;

        AnimationSequence = DOTween.Sequence();
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