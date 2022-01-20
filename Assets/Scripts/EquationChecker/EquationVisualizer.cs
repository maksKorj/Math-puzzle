using LevelBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationVisualizer : MonoBehaviour
{
    [SerializeField] private GridElement _gridElement;
    [SerializeField] private UnitGridAmountChecker _gridAmountChecker;
    [SerializeField] private VictoryConditionHandler[] _victoryConditionHandlers;
    [SerializeField] private MultiplyEquationEffect _multiplyEquationEffect;

    private WaitForSeconds _selectDelay, _hideDelay, _showDelay, _multiplyEffectDelay;
    private WaitWhile _victoryDisplayDelay;
    private EquationChecker _equationChecker;
    private VictoryConditionHandler _victoryConditionHandler;

    public event Action OnEndChecking;
    public event Action OnEmptyGrid;
    public event Action OnWin;
    public event Action OnLose;

    private void Awake()
    {
        _selectDelay = new WaitForSeconds(_gridElement.SelectingTime);
        _hideDelay = new WaitForSeconds(_gridElement.HidingTime);
        _showDelay = new WaitForSeconds(_gridElement.ShowingTime);
        _victoryDisplayDelay = new WaitWhile(() => _victoryConditionHandler.IsPlaying == true);
        _multiplyEffectDelay = new WaitForSeconds(_multiplyEquationEffect.EffectTime);

        _equationChecker = GetComponent<EquationChecker>();
    }

    private void Start()
    {
        for(int i = 0; i < _victoryConditionHandlers.Length; i++)
        {
            if(_victoryConditionHandlers[i].IsActiveCondition)
            {
                _victoryConditionHandler = _victoryConditionHandlers[i];
                return;
            }
        }
    }

    public void EndShowing()
        => StartCoroutine(EndShow());

    public void Show(Dictionary<int, List<GridElement>> equations)
        => StartCoroutine(ShowEquation(equations));

    private IEnumerator ShowEquation(Dictionary<int, List<GridElement>> equations)
    {
        int amount = 0;

        for (int i = 0; i < equations.Count; i++)
        {
            Show(equations[i]);
            amount++;
            yield return _selectDelay;
        }

        for(int i = 0; i < equations.Count; i++)
            Hide(equations[i]);

        yield return _hideDelay;

        while(_equationChecker.HasCompletedEquation(out equations) != false)
        {
            for (int i = 0; i < equations.Count; i++)
            {
                Show(equations[i]);
                amount++;
                yield return _selectDelay;
            }

            for (int i = 0; i < equations.Count; i++)
                Hide(equations[i]);

            yield return _hideDelay;
        }

        if(amount > 1)
        {
            _multiplyEquationEffect.ShowEffect(amount);
            yield return _multiplyEffectDelay;
        }

        StartCoroutine(EndShow());
    }

    private IEnumerator EndShow()
    {
        _gridAmountChecker.CountAmount();
        
        if(_victoryConditionHandler != null && _victoryConditionHandler.WasUpdate)
        {
            _victoryConditionHandler.UpdateGameUi();
            yield return _victoryDisplayDelay;

            if (_victoryConditionHandler.IsWin)
            {
                OnWin?.Invoke();
                yield break;
            }
        }

        if(_gridAmountChecker.IsFull)
        {
            OnLose?.Invoke();
            yield break;
        }
        
        if (_gridAmountChecker.IsEmpty)
        {
            OnEmptyGrid?.Invoke();
            yield return _showDelay;
            OnEndChecking?.Invoke();
        }
        else
            OnEndChecking?.Invoke();
    }

    private void Show(List<GridElement> equation)
    {
        for (int i = 0; i < equation.Count; i++)
            equation[i].SelectAtEquation();

        CheckWinCondition(equation);
    }

    private void Hide(List<GridElement> equation)
    {
        for (int i = 0; i < equation.Count; i++)
            equation[i].PlayHideEffect();
    }

    private void CheckWinCondition(List<GridElement> gridElements)
    {
        if(_victoryConditionHandler != null)
        {
            _victoryConditionHandler.UpdateConditionState(gridElements);
        }
    }
}
