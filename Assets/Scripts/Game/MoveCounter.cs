using UnityEngine;
using TMPro;

public class MoveCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountDisplay;
    [SerializeField] private LevelPropertyHandler _levelPropertyHandler;
    [SerializeField] private Color _defaultColor, _minAmountColor;

    int _moveAmount;
    

    public bool IsRunOutMoves => _moveAmount <= 0;

    private void Awake()
    {
        _moveAmount = _levelPropertyHandler.AmountOfMoves();
        UpdateText();
    }

    public void RemoveMove()
    {
        _moveAmount--;
        UpdateText();
    }

    public void AddAmount(int amount = 1)
    {
        _moveAmount += amount;
        UpdateText();
    }

    private void UpdateText()
    {
        if (_moveAmount <= 3)
            _amountDisplay.color = _minAmountColor;
        else
            _amountDisplay.color = _defaultColor;

        _amountDisplay.text = _moveAmount.ToString();
    }
}
