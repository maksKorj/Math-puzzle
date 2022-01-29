using LevelBuilder;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuideHandler : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private GuideButton _guideButton;
    [SerializeField] private TextMeshProUGUI _hintDisplay;
    [SerializeField] private List<GuideItem> _guideItems;

    private GridBuilder _gridBuilder;
    private SymbolGiverVisual _symbolGiverVisual;

    private void OnEnable()
    {
        if(_gridBuilder == null)
        {
            _gridBuilder = FindObjectOfType<GridBuilder>();
            FindObjectOfType<GridAnimation>().OnEndStartAnimation += Show;
            gameObject.SetActive(false);
        }
    }

    private void Show()
    {
        if(_guideItems.Count == 0)
        {
            Debug.LogError("There is not guide available!!!");
            return;
        }

        gameObject.SetActive(true);

        if(_symbolGiverVisual == null)
        {
            _symbolGiverVisual = FindObjectOfType<SymbolGiverVisual>();
            _symbolGiverVisual.OnEndGivingSymbol += Show;
        }

        _hintDisplay.text = _guideItems[0].TextHint;

        var gridButton = _gridBuilder.GridElement(_guideItems[0].GridPosition);
        _guideItems.RemoveAt(0);

        _hand.PlayHandAnimation(gridButton.WorldPosition);
        _guideButton.SetPosition((GridButton)gridButton, this);
    }

    public void Hide()
    {
        _hand.StopAnimation();

        if(_guideItems.Count == 0)
            _symbolGiverVisual.OnEndGivingSymbol -= Show;

        gameObject.SetActive(false);
    }
}

[System.Serializable]
public class GuideItem
{
    [SerializeField] private Vector2Int _gridPosition;
    [SerializeField] private string _textHint;

    public Vector2Int GridPosition => _gridPosition;
    public string TextHint => _textHint;
}