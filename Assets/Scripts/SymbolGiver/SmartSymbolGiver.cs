using UnityEngine;
using LevelBuilder;
using System.Collections.Generic;

public class SmartSymbolGiver : MonoBehaviour
{
    [SerializeField] private EquationBuilder _equationBuilder;
    [SerializeField] private EquationCreator _equationCreator;

    private List<GridContent> _equation = new List<GridContent>();

    public GridContent GetElement()
    {
        GridContent gridContent;

        if (_equationBuilder.HasAvailableContent(out gridContent))
            return gridContent;

        if (_equation.Count <= 0)
            _equation = _equationCreator.GetEquation();

        gridContent = _equation[0];
        _equation.RemoveAt(0);

        return gridContent;
    }
}
