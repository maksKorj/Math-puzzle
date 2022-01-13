using System.Collections.Generic;
using UnityEngine;

public class EquationCreator : MonoBehaviour
{
    [SerializeField] private Symbols _symbols;
    [SerializeField] private LevelPropertyHandler _levelPropertyHandler;

    private int _minSize;

    private void Awake()
    {
        var size = _levelPropertyHandler.Size();
        _minSize = Mathf.Min(size.x, size.y);
    }

    public List<GridContent> GetEquation()
    {
        var mathSign = _symbols.MathSigns[Random.Range(0, _symbols.MathSigns.Length)];
        var comparsion = _symbols.ComparisonSigns[Random.Range(0, _symbols.ComparisonSigns.Length)];

        int equalTo = mathSign.EqualTo(out int firstNumber, out int secondNumber, _minSize);

        if (comparsion.CanCorrecting(mathSign.EqualTo(firstNumber, secondNumber), equalTo, out int correctionRightPart))
            equalTo = correctionRightPart;
        else
            comparsion = EqualSign();

        return FormList(firstNumber, mathSign, secondNumber, comparsion, equalTo);
    }

    private List<GridContent> FormList(int firstNumber, MathSign mathSign, int secondNumber, ComparisonSign comparisonSign, int equalTo)
    {
        var listGridContents = new List<GridContent>();

        AddToList(listGridContents, firstNumber);
        listGridContents.Add(mathSign);
        AddToList(listGridContents, secondNumber);
        listGridContents.Add(comparisonSign);
        AddToList(listGridContents, equalTo);

        return listGridContents;
    }

    private void AddToList(List<GridContent> gridContents, int number)
    {
        foreach (var gridContent in GetNumber(number))
            gridContents.Add(gridContent);
    }

    private IEnumerable<GridContent> GetNumber(int number)
    {
        if (number < 10)
            yield return GetGridNumber(number);
        else
        {
            yield return GetGridNumber(number / 10);
            yield return GetGridNumber(number % 10);
        }
    }

    private GridContent GetGridNumber(int digit)
    {
        for(int i = 0; i < _symbols.Numbers.Length; i++)
        {
            if (_symbols.Numbers[i].Num == digit)
                return _symbols.Numbers[i];
        }

        return null;
    }

    private ComparisonSign EqualSign()
    {
        for(int i = 0; i < _symbols.ComparisonSigns.Length; i++)
        {
            if (_symbols.ComparisonSigns[i] is EqualSign)
                return _symbols.ComparisonSigns[i];
        }

        return null;
    }
}

