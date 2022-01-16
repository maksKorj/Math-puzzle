using LevelBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationVisualizer : MonoBehaviour
{
    [SerializeField] private GridElement _gridElement;
    [SerializeField] private UnitGridAmountChecker _gridAmountChecker;

    WaitForSeconds _selectDelay, _hideDelay, _showDelay;

    public event Action OnEndChecking;
    public event Action OnEmptyGrid;

    private void Awake()
    {
        _selectDelay = new WaitForSeconds(_gridElement.SelectingTime);
        _hideDelay = new WaitForSeconds(_gridElement.HidingTime);
        _showDelay = new WaitForSeconds(_gridElement.ShowingTime);
    }

    public void EndShowing()
    {
        _gridAmountChecker.CountAmount();
        OnEndChecking?.Invoke();
    }

    public void Show(Dictionary<int, List<GridElement>> equations)
        => StartCoroutine(ShowEquation(equations));

    private IEnumerator ShowEquation(Dictionary<int, List<GridElement>> equations)
    {
        for (int i = 0; i < equations.Count; i++)
        {
            Show(equations[i]);
            yield return _selectDelay;
        }

        for(int i = 0; i < equations.Count; i++)
            Hide(equations[i]);

        yield return _hideDelay;

        _gridAmountChecker.CountAmount();
        if(_gridAmountChecker.IsEmpty)
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
            equation[i].SelectEquation();
    }

    private void Hide(List<GridElement> equation)
    {
        for (int i = 0; i < equation.Count; i++)
            equation[i].HideAfterSelect();
    }
}
