using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PresentBoxPopUp : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _present;
    [SerializeField] private PresentPopUp _presentPopUp;
    [SerializeField] private GameObject _button;

    private Tweener _shake;

    public void BlockRaycast() => _background.enabled = true;

    public void UnBlockRaycast() => _background.enabled = false;

    public void ShowPopUp() 
        => _background.DOFade(1, 1f).OnComplete(ShowPresent);

    private void ShowPresent()
    {
        _present.gameObject.SetActive(true);
        _shake = _present.DOShakeAnchorPos(1f, 10, 1).SetLoops(-1);
        _button.SetActive(true);
    }

    public void GivePresent()
    {
        _shake.Kill();
        gameObject.SetActive(false);

        _presentPopUp.Open();
    }

}
