using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    private RectTransform _rectTransform;

    Tween _animation;

    public void PlayHandAnimation(Vector2 position)
    {
        if (_rectTransform == null)
            _rectTransform = GetComponent<RectTransform>();

        gameObject.SetActive(true);
        _rectTransform.anchoredPosition = position;
        _animation = _rectTransform.DOPunchScale(Vector3.one * 0.5f, 1.5f, 1).SetLoops(-1);
    }

    public void StopAnimation()
    {
        _animation.Kill();
        gameObject.SetActive(false);
    }
}
