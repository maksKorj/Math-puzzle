using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettingsPopup : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _circleTransform;

    public void OpenPopUp()
    {
        _background.enabled = true;
        gameObject.SetActive(true);
        OpenAmimation();
    }

    private void OpenAmimation()
    {
        _background.DOFade(0.4f, 0.8f).SetEase(Ease.InBounce);
        _circleTransform.DOScale(Vector2.one, 1f).SetEase(Ease.InCubic);
    }

    public void Close()
    {
        _background.DOFade(0f, 0.8f).SetEase(Ease.OutBounce);
        _circleTransform.DOScale(Vector2.one * 0.5f, 1f).SetEase(Ease.OutCubic).OnComplete(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        _background.enabled = false;
    }
}
