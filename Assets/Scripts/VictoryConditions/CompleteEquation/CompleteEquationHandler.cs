using LevelBuilder;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CompleteEquationHandler : VictoryConditionHandler
{
    private int _totalFromMove = 0;
    private WaitForSeconds _delay = new WaitForSeconds(1);

    public override void UpdateConditionState(List<GridElement> gridElements)
    {
        _totalFromMove++;

        IsWin = _amountToWin <= _currentAmount + _totalFromMove;

        WasUpdate = true;
    }

    public override void UpdateGameUi()
    {
        IsPlaying = true;
        StartCoroutine(WaitAndFill());
    }

    private IEnumerator WaitAndFill()
    {
        while(_totalFromMove > 0)
        {
            _totalFromMove--;
            _fillImage.DOFillAmount(1f, 1f).OnComplete(UpdateText);

            yield return _delay;
        }

        IsPlaying = false;
        WasUpdate = false;
    }

    protected override void UpdateText()
    {
        _currentAmount++;
        _gameAmountDisplay.text = $"{_currentAmount}";
        _fillImage.fillAmount = 0;
    }

    protected override void StartUpdateUi(int amount)
    {
        _currentAmount = 0;
        //_startAmountDisplay.text = $"{amount}";
        _gameAmountDisplay.text = "0";
    }
}
