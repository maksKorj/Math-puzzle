using UnityEngine;
using DG.Tweening;

public class LabelAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 _direction = Vector2.up * 10;
    private Vector2 _startPositon;

    Tweener _animation;

    public void PlayAnimation()
    {
        gameObject.SetActive(true);

        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPositon = _rectTransform.anchoredPosition;
        }

        _animation = _rectTransform.DOPunchAnchorPos(_direction, 1f, 2).SetLoops(-1);
    }

    public void StopAnimation()
    {
        _animation.Kill();
        _rectTransform.anchoredPosition = _startPositon;

        gameObject.SetActive(false);
    }
}
