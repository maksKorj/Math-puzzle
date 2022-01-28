using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MultiplyEquationEffect : MonoBehaviour
{
    [SerializeField] private float _unitAnimationDuration = 1f;
    [SerializeField] private Sprite[] _textEffects;
    [SerializeField] private Image _image;

    private RectTransform _rectTransform;
    private Vector2 _startPosition, _middlePosition,_endPostion;
    private WaitForSeconds _delay = new WaitForSeconds(0.5f);

    public float EffectTime => _unitAnimationDuration * 1.5f;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _startPosition = _rectTransform.anchoredPosition;
        _middlePosition = new Vector2(0, _startPosition.y);
        _endPostion = new Vector2(_startPosition.x * (-1), _startPosition.y);

        gameObject.SetActive(false);
    }

    public void ShowEffect(int amount)
    {
        gameObject.SetActive(true);
        _rectTransform.anchoredPosition = _startPosition;
        _image.sprite = _textEffects[Random.Range(0, _textEffects.Length)];
        _image.SetNativeSize();

        _rectTransform.DOAnchorPos(_middlePosition, _unitAnimationDuration / 2).SetEase(Ease.OutBack).OnComplete(() => StartCoroutine(WaitAndMoveForward()));
    }

    private IEnumerator WaitAndMoveForward()
    {
        yield return _delay;
        AudioController.Instance.PlaySound(SoundItem.COMBO);
        _rectTransform.DOAnchorPos(_endPostion, _unitAnimationDuration / 2).SetEase(Ease.InFlash).OnComplete(() => gameObject.SetActive(false));
    }
}
