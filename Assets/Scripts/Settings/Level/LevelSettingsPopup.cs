using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettingsPopup : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _circleTransform;
    [SerializeField] private Button _backgroundButton;

    private void Start()
    {
        BackButton.Instance.SetDefaultAction(OpenPopUp);
        gameObject.SetActive(false);
    }

    public void OpenPopUp()
    {
        _background.enabled = true;
        gameObject.SetActive(true);
        OpenAmimation();

        BackButton.Instance.AddBackButtonAction(CloseAnimation);
    }

    private void OpenAmimation()
    {
        _background.DOFade(0.8f, 0.8f);
        _circleTransform.DOScale(Vector2.one, 1f).SetEase(Ease.InCubic).OnComplete(() => _backgroundButton.enabled = true); 
    }

    public void Close()
    {
        CloseAnimation();
        BackButton.Instance.RemoveLastAction();
    }

    private void CloseAnimation()
    {
        _background.DOFade(0f, 0.8f);
        _circleTransform.DOScale(Vector2.one * 0.5f, 1f).SetEase(Ease.OutCubic).OnComplete(Disable);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        _background.enabled = false;
        _backgroundButton.enabled = false;
    }
}
