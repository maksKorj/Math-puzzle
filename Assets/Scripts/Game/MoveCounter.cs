using UnityEngine;
using TMPro;

public class MoveCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountDisplay;
    [SerializeField] private LevelConfig _levelConfig;

    int _moveAmount;

    public bool IsRunOutMoves => _moveAmount <= 0;

    private void Awake()
    {
        _moveAmount = _levelConfig.GetLevelConfig(PlayerSaver.LoadPlayerLevel()).AmountOfMoves;
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
            _amountDisplay.color = Color.red;
        else
            _amountDisplay.color = Color.black;

        _amountDisplay.text = _moveAmount.ToString();
    }
}
