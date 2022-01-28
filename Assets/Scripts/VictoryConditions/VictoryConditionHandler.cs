using LevelBuilder;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class VictoryConditionHandler : ClearFieldHandler
{
    [SerializeField] protected TextMeshProUGUI _startAmountDisplay;
    [SerializeField] protected TextMeshProUGUI _gameAmountDisplay;
    [SerializeField] protected Image _fillImage;
    [SerializeField] protected EquationVisualizer _equationVisualizer;

    protected int _amountToWin;
    protected int _currentAmount;

    public bool IsWin { get; protected set; }
    public bool IsPlaying { get; protected set; }
    public bool WasUpdate { get; protected set; }
    
    public void Initialize(int amount)
    {
        ShowStartUi();
        _amountToWin = amount;
        _equationVisualizer.SetVictoryCondition(this);

        StartUpdateUi(amount);
    }

    protected abstract void UpdateText();
    protected abstract void StartUpdateUi(int amount);
    public abstract void UpdateGameUi();
    public abstract void UpdateConditionState(List<GridElement> gridElements);
}
