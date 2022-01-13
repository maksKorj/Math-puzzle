using UnityEngine;
using LevelBuilder;
using System.Collections.Generic;

public class SmartSymbolGiver : MonoBehaviour
{
    [SerializeField] private GridBuilder _gridBuilder;
    [SerializeField] private EquationCreator _equationCreator;

    //private Dictionary<int, List<GridContent>> _equations = new Dictionary<int, List<GridContent>>();
    private List<GridContent> _equation = new List<GridContent>();

    public GridContent GetElement()
    {
        if (_equation.Count <= 0)
            _equation = _equationCreator.GetEquation();

        var gridContent = _equation[0];
        _equation.RemoveAt(0);

        return gridContent;
    }

    private void CheckHorizontal(int x, int y)
    {
        
    }
}
