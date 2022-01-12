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

    #region Horizontal
    private void CheckHorizontal(int y)
    {
        for(int x = 0; x < _gridBuilder.SizeX; x++)
        {
            if(_gridBuilder.GridElement(x, y) != null && _gridBuilder.GridElement(x, y).IsTaken—omparisonSigns)
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
            if(_gridBuilder.GridElement(x, comparisonSignGridPos.y).IsTaken &&
                _gridBuilder.GridElement(x, comparisonSignGridPos.y).IsTaken—omparisonSigns == false)
            {
                _firstPart.Add(_gridBuilder.GridElement(x, comparisonSignGridPos.y));
            }
            else
            {
                break;
            }
        }
        _firstPart.Reverse();

        for (int x = comparisonSignGridPos.x + 1; x < _gridBuilder.SizeX - 1; x++)
        {
            if (_gridBuilder.GridElement(x, comparisonSignGridPos.y).IsTaken &&
                _gridBuilder.GridElement(x, comparisonSignGridPos.y).IsTaken—omparisonSigns == false)
            {
                _secondPart.Add(_gridBuilder.GridElement(x, comparisonSignGridPos.y));
            }
            else
            {
                break;
            }
        }

        CheckParts(comparison);
    }
    #endregion

    #region Vertical
    private void CheckVertical(int x)
    {
        for (int y = 0; y < _gridBuilder.SizeY; y++)
        {
            if (_gridBuilder.GridElement(x, y) != null && _gridBuilder.GridElement(x, y).IsTaken—omparisonSigns)
            {
                CheckVertical(_gridBuilder.GridElement(x, y), new Vector2Int(x, y));
            }
        }
    }

    private void CheckVertical(GridElement comparison, Vector2Int comparisonSignGridPos)
    {
        _firstPart.Clear();
        _secondPart.Clear();

        for (int y = comparisonSignGridPos.y + 1; y < _gridBuilder.SizeY - 1; y++)
        {
            if (_gridBuilder.GridElement(comparisonSignGridPos.x, y).IsTaken &&
                _gridBuilder.GridElement(comparisonSignGridPos.x, y).IsTaken—omparisonSigns == false)
            {
                _firstPart.Add(_gridBuilder.GridElement(comparisonSignGridPos.x, y));
            }
            else
            {
                break;
            }
        }
        _firstPart.Reverse();

        for (int y = comparisonSignGridPos.y - 1; y >= 0; y--)
        {
            if (_gridBuilder.GridElement(comparisonSignGridPos.x, y).IsTaken &&
                _gridBuilder.GridElement(comparisonSignGridPos.x, y).IsTaken—omparisonSigns == false)
            {
                _secondPart.Add(_gridBuilder.GridElement(comparisonSignGridPos.x, y));
            }
            else
            {
                break;
            }
        }

        CheckParts(comparison);
    }
    #endregion

    private void CheckParts(GridElement comparison)
    {
        if (_firstPart.Count > 0 && _secondPart.Count > 0)
        {
            var comparisonSign = (ComparisonSign)comparison.GridContent;

            if (comparisonSign.IsTrueExpression(_numberCalculator.GetNumber(_firstPart, true),
                _numberCalculator.GetNumber(_secondPart, false)))
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
