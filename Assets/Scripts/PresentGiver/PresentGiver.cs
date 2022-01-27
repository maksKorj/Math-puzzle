using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PresentGiver : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _amountDisplay;
    [SerializeField] private PresentBoxPopUp _presentBoxPopUp;
    [SerializeField] private float[] _fillStages;

    private int _amountToGetPresent = 5;
    private int _currentAmount;
    private WaitForSeconds _delay = new WaitForSeconds(0.1f);

    public void LevelCompleted() => StartCoroutine(UpdateAmount());


    private IEnumerator UpdateAmount()
    {
        Action action;
        _currentAmount = PlayerSaver.LoadCompleteLevelNumber();

        _presentBoxPopUp.BlockRaycast();

        _progressBar.fillAmount = (float)_currentAmount / _amountToGetPresent;
        _amountDisplay.text = $"<color=#ffffff>{_currentAmount}</color>/{_amountToGetPresent}";

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

        yield return _delay;

        _progressBar.DOFillAmount(_fillStages[_currentAmount - 1], 1f).OnComplete(() => UpdateText(action));
    }

    private void UpdateText(Action popUpAction)
    {
        _amountDisplay.text = $"<color=#ffffff>{_currentAmount}</color>/{_amountToGetPresent}";
        popUpAction();
    }
}
