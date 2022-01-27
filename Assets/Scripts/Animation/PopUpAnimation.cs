using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class PopUpAnimation : MonoBehaviour
{
    [SerializeField] protected Image _background;

    protected RectTransform _rectTransform;

    public void Open()
    {
        EnableObject();
        OpenAmimation();
    }

    public void Open(Action onEndOpen)
    {
        EnableObject();
        OpenAmimation(onEndOpen);
    }

    private void EnableObject()
    {
        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.localScale = Vector3.zero;
        }

        _background.enabled = true;
        gameObject.SetActive(true);
    }

    protected virtual void OpenAmimation()
    {
        _background.DOFade(0.8f, 0.4f).SetEase(Ease.InCubic);
        _rectTransform.DOScale(Vector2.one, 0.5f).SetEase(Ease.InCubic);
    }

    private void OpenAmimation(Action onEndAnimation)
    {
        _background.DOFade(0.8f, 0.4f).SetEase(Ease.InCubic);
        _rectTransform.DOScale(Vector2.one, 0.5f).SetEase(Ease.InCubic).OnComplete(() => onEndAnimation());
    }

    public void Close()
    {
        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        CloseAnimation();
    }

    protected virtual void CloseAnimation()
    {
        _background.DOFade(0f, 0.4f).SetEase(Ease.InCubic);
        _rectTransform.DOScale(Vector2.zero, 0.5f).SetEase(Ease.OutCubic).OnComplete(Disable);
    }

    protected virtual void Disable()
    {
        gameObject.SetActive(false);
        _background.enabled = false;
    }
}
