using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PresentGiver : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _amountDisplay;
    [SerializeField] private PresentBoxPopUp _presentBoxPopUp;

    private int _amountToGetPresent = 5;
    private int _currentAmount;

    public void LevelCompleted()
    {
        Action action;
        _currentAmount = PlayerSaver.LoadCompleteLevelNumber();

        _presentBoxPopUp.BlockRaycast();

        _progressBar.fillAmount = (float)_currentAmount / _amountToGetPresent;
        _amountDisplay.text = $"{_currentAmount} / {_amountToGetPresent}";

        _currentAmount++;
        if (_currentAmount >= _amountToGetPresent)
        {
            action = _presentBoxPopUp.ShowPopUp;
            PlayerSaver.SaveCompleteLevelNumber(0);
        }
        else
        {
            action = _presentBoxPopUp.UnBlockRaycast;
            PlayerSaver.SaveCompleteLevelNumber(_currentAmount);
        }

        _progressBar.DOFillAmount((float)_currentAmount / _amountToGetPresent, 1f).OnComplete(() => UpdateText(action));
    }

    private void UpdateText(Action popUpAction)
    {
        _amountDisplay.text = $"{_currentAmount} / {_amountToGetPresent}";
        popUpAction();
    }
}
