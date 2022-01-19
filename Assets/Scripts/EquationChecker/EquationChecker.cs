using UnityEngine;
using LevelBuilder;
using System.Collections.Generic;

public class EquationChecker : MonoBehaviour
{
    [SerializeField] private GridBuilder _gridBuilder;

    private EquationVisualizer _equationVisualizer;
    private NumberCalculator _numberCalculator = new NumberCalculator();
    private List<GridElement> _firstPart = new List<GridElement>();
    private List<GridElement> _secondPart = new List<GridElement>();
    private Dictionary<int, List<GridElement>> _equations = new Dictionary<int, List<GridElement>>();

    private void Awake()
        => _equationVisualizer = GetComponent<EquationVisualizer>();

    public void Check()
    {
        _equations.Clear();
        CheckAllGrid();

        if (_equations.Count > 0)
            _equationVisualizer.Show(_equations);
        else
            _equationVisualizer.EndShowing();
    }

    public void Check(Vector2Int gridPosition)
    {
        _equations.Clear();

        CheckHorizontal(gridPosition.y);
        CheckVertical(gridPosition.x);

        if (_equations.Count > 0)
            _equationVisualizer.Show(_equations);
        else
            _equationVisualizer.EndShowing();
    }

    public bool HasCompletedEquation(out Dictionary<int, List<GridElement>> equations)
    {
        _equations.Clear();
        CheckAllGrid();
        equations = _equations;

        return _equations.Count > 0;
    }

    private void CheckAllGrid()
    {
        for (int x = 0; x < _gridBuilder.SizeX; x++)
        {
            for (int y = 0; y < _gridBuilder.SizeY; y++)
            {
                if (x == y)
                {
                    CheckHorizontal(y);
                    CheckVertical(x);
                }
            }
        }
    }

    public void RemoveCorrectEquation(Vector2Int gridPosition)
    {
        _equations.Clear();

        CheckHorizontal(gridPosition.y);
        CheckVertical(gridPosition.x);

        for (int i = 0; i < _equations.Count; i++)
            Remove(_equations[i]);
    }

    private void Remove(List<GridElement> equation)
    {
        for (int i = 0; i < equation.Count; i++)
            equation[i].RemoveGridContent();
    }

    #region Horizontal
    private void CheckHorizontal(int y)
    {
        for(int x = 0; x < _gridBuilder.SizeX; x++)
        {
            if(_gridBuilder.GridElement(x, y) != null && _gridBuilder.GridElement(x, y).IsTakenComparisonSigns)
            {
                CheckHorizontal(_gridBuilder.GridElement(x, y), new Vector2Int(x, y));
            }  
        }
    }

    private void CheckHorizontal(GridElement comparison, Vector2Int comparisonSignGridPos)
    {
        _firstPart.Clear();
        _secondPart.Clear();

        for (int x = comparisonSignGridPos.x - 1; x >= 0; x--)
        {
            if (_gridBuilder.GridElement(x, comparisonSignGridPos.y) == null)
                continue;

            if (CanAddElement(x, comparisonSignGridPos.y, _firstPart) == false)
                break;
        }
        _firstPart.Reverse();

        for (int x = comparisonSignGridPos.x + 1; x < _gridBuilder.SizeX; x++)
        {
            if (_gridBuilder.GridElement(x, comparisonSignGridPos.y) == null)
                continue;

            if (CanAddElement(x, comparisonSignGridPos.y, _secondPart) == false)
                break;
        }

        CheckParts(comparison);
    }
    #endregion

    #region Vertical
    private void CheckVertical(int x)
    {
        for (int y = 0; y < _gridBuilder.SizeY; y++)
        {
            if (_gridBuilder.GridElement(x, y) != null && _gridBuilder.GridElement(x, y).IsTakenComparisonSigns)
            {
                CheckVertical(_gridBuilder.GridElement(x, y), new Vector2Int(x, y));
            }
        }
    }

    private void CheckVertical(GridElement comparison, Vector2Int comparisonSignGridPos)
    {
        _firstPart.Clear();
        _secondPart.Clear();

        for (int y = comparisonSignGridPos.y + 1; y < _gridBuilder.SizeY; y++)
        {
            if (_gridBuilder.GridElement(comparisonSignGridPos.x, y) == null)
                continue;

            if (CanAddElement(comparisonSignGridPos.x, y, _firstPart) == false)
                break;
        }
        _firstPart.Reverse();

        for (int y = comparisonSignGridPos.y - 1; y >= 0; y--)
        {
            if (_gridBuilder.GridElement(comparisonSignGridPos.x, y) == null)
                continue;

            if (CanAddElement(comparisonSignGridPos.x, y, _secondPart) == false)
                break;
        }

        CheckParts(comparison);
    }
    #endregion

    private bool CanAddElement(int x, int y, List<GridElement> gridElements)
    {
        if (_gridBuilder.GridElement(x, y).IsTaken &&
                _gridBuilder.GridElement(x, y).IsTakenComparisonSigns == false)
        {
            gridElements.Add(_gridBuilder.GridElement(x, y));
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckParts(GridElement comparison)
    {
        if (_firstPart.Count > 0 && _secondPart.Count > 0)
        {
            var comparisonSign = (ComparisonSign)comparison.GridContent;
            int? firstNumber = _numberCalculator.GetNumber(_firstPart, true);
            int? secondNumber = _numberCalculator.GetNumber(_secondPart, false);

            //Debug.Log(firstNumber + " " + secondNumber);

            if(firstNumber == null || secondNumber == null)
                return;

            if (comparisonSign.IsTrueExpression((int)firstNumber, (int)secondNumber))
            {
                _equations.Add(_equations.Count, GetList(comparison));
            }
        }
    }

    #region List
    private List<GridElement> GetList(GridElement comparison)
    {
        var fullList = new List<GridElement>();

        AddToList(fullList, _firstPart);
        fullList.Add(comparison);
        AddToList(fullList, _secondPart);

        return fullList;
    }

    private void AddToList(List<GridElement> full, List<GridElement> addingList)
    {
        for(int i = 0; i < addingList.Count; i++)
            full.Add(addingList[i]);
    }
    #endregion
}
