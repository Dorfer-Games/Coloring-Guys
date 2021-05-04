using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AnimationMoneyRewarded : MonoBehaviour
{
    [SerializeField] private float durationAnimation;
    [SerializeField] private Transform startPoint, target;

    public float Duration => durationAnimation * 2;

    void OnEnable()
    {
        transform.position = startPoint.position;

        //var AnimationSequence = DOTween.Sequence();
        //AnimationSequence.Join(transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), durationAnimation * 0.8f));
        //AnimationSequence.Join(transform.DOMove(target.position, durationAnimation * 0.8f));
        //AnimationSequence.Append(transform.DOScale(new Vector3(0.137f, 0.137f, 0.137f), durationAnimation / 5));
        //AnimationSequence.OnComplete(() => Disabled());
        //AnimationSequence.SetEase(Ease.Linear);
        //AnimationSequence.Play();

        StartCoroutine(AnimationRoutine());
    }

    public void SetStartPoint(Transform point)
    {
        startPoint = point;
    }

    private void Disabled()
    {
        gameObject.SetActive(false);
    }

    IEnumerator AnimationRoutine()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var sequence = DOTween.Sequence();
            var child = transform.GetChild(i);

            sequence.Append(child.DOScale(Vector3.one * 1.25f, 0f));
            sequence.Append(child.DOMove(target.position, durationAnimation).SetEase(Ease.Linear));
            sequence.Join(child.DOScale(Vector3.one * 0.25f, durationAnimation).SetEase(Ease.InQuart));
            sequence.OnComplete(() => child.gameObject.SetActive(false));
            sequence.Play();

            yield return new WaitForSeconds(0.1f);
        }
    }
}