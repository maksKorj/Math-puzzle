using LevelBuilder;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RemoveChipAmountHandler : VictoryConditionHandler
{
    public override void UpdateConditionState(List<GridElement> gridElements)
    {
        _currentAmount += gridElements.Count;
        _currentAmount = Mathf.Min(_currentAmount, _amountToWin);

        IsWin = _amountToWin <= _currentAmount;

        WasUpdate = true;
    }

    public override void UpdateGameUi()
    {
        IsPlaying = true;
        _fillImage.DOFillAmount((float)_currentAmount / _amountToWin, 1f).OnComplete(UpdateText);
    }

    protected override void UpdateText()
    {
        _gameAmountDisplay.text = $"{_currentAmount} / {_amountToWin}";
        IsPlaying = false;
        WasUpdate = false;
    }

    protected override void StartUpdateUi(int amount)
    {
        _currentAmount = 0;
        _startAmountDisplay.text = $"{_currentAmount} / {amount}";
        _gameAmountDisplay.text = $"{_currentAmount} / {amount}";
    }
}
