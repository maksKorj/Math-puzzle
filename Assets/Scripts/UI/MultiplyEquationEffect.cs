using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MultiplyEquationEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplay;
    [SerializeField] private float _unitAnimationDuration = 1f;

    private RectTransform _rectTransform;
    private IntToText _intToText;
    private Vector2 _startPosition, _middlePosition,_endPostion;
    private WaitForSeconds _delay = new WaitForSeconds(0.5f);

    public float EffectTime => _unitAnimationDuration * 2.5f;

    private void Awake()
    {
        _intToText = new IntToText();
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
        _textDisplay.text = _intToText.ConvertInt(amount);

        _rectTransform.DOAnchorPos(_middlePosition, 1f).SetEase(Ease.OutBack).OnComplete(() => StartCoroutine(WaitAndMoveForward()) );
    }

    private IEnumerator WaitAndMoveForward()
    {
        yield return _delay;
        _rectTransform.DOAnchorPos(_endPostion, 1f).SetEase(Ease.InFlash).OnComplete(() => gameObject.SetActive(false));
    }
}

public class IntToText
{
    private string[] _names;

    public IntToText()
    {
        _names = new string[] { "DOUBLE", "TRIPLE", "QUADRUPLE", "QUINTUPLE" };
    }

    public string ConvertInt(int amount)
    {
        if(amount >= _names.Length || amount < 2)
        {
            Debug.LogError("Amount is incorrect!");
        }

        return _names[amount - 2];
    }
}
