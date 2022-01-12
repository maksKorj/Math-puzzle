using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SymbolGiverVisual : MonoBehaviour
{
    [SerializeField] private Image _symbol;
    [SerializeField] private Image _background;
    [SerializeField] private RectTransform _point;

    private RectTransform _symbolTransform, _backgroundTransform;
    private Vector2 _startPosition = new Vector2(0, 0);

    public event Action OnEndGivingSymbol;

    private void Awake()
    {
        _symbolTransform = GetComponent<RectTransform>();
    }

    public bool IsFading { get; private set; }

    public void ShowSymbol(GridContent gridContent)
    {
        if(_backgroundTransform == null)
            _backgroundTransform = GetComponent<RectTransform>();

        _backgroundTransform.anchoredPosition = _point.anchoredPosition;

        SetImage(_background, gridContent.BackgroundSprite, gridContent.BackgroundColor);
        SetImage(_symbol, gridContent.SymbloSprite, gridContent.SymbolColor);

        _backgroundTransform.DOAnchorPos(_startPosition, 1f).SetEase(Ease.OutBack).OnComplete(() => OnEndGivingSymbol?.Invoke());
    }

    private void SetImage(Image image, Sprite sprite, Color color)
    {
        image.enabled = true;
        image.sprite = sprite;
        image.color = color;
    }

    public void Hide()
    {
        IsFading = true;
        _backgroundTransform.DOScale(0.75f, 0.5f);
        _symbolTransform.DOScale(0.75f, 0.5f).OnComplete(HideImages);
    }

    private void HideImages()
    {
        IsFading = false;
        _background.enabled = false;
        _symbol.enabled = false;

        _backgroundTransform.localScale = Vector3.one;
        _symbolTransform.localScale = Vector3.one;
    }
}
