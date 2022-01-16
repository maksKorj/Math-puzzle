using LevelBuilder;
using System.Collections.Generic;
using UnityEngine;

public class NumberCalculator
{
    private List<NumberMathSign> _equation;

    public NumberCalculator()
        => _equation = new List<NumberMathSign>();

    public int? GetNumber(List<GridElement> part, bool isFirstPart)
    {
        _equation.Clear();

        if (CanGetNumber(part, isFirstPart) == false)
            return null;

        int startIndex = 0;

        for (int i = 0; i < part.Count; i++)
            startIndex = CheckPart(part, startIndex, i);

        if (MathSignAmountZero() == false && startIndex < part.Count)
            _equation.Add(new NumberMathSign(GetNumber(part, startIndex, part.Count)));

        if (_equation.Count == 1)
            return _equation[0].Number;

        if (_equation.Count == 0)
        {
            return GetNumber(part, 0, part.Count);
        }
        else
        {
            if (_equation[0].IsMathSign(out MathSign mathSign))
            {
                if (mathSign is Subtraction)
                {
                    _equation[1].NegativeNumber();
                    _equation.RemoveAt(0);
                }
                else
                    Debug.LogError("First sign is not subtraction!!!");
            }

            while (HasMultiplicationOrDivision() != false);

            while (_equation.Count != 1)
            {
                for (int i = 0; i < _equation.Count; i++)
                {
                    if (_equation[i].IsMathSign(out mathSign))
                    {
                        CalculateAndRemove(i, mathSign.EqualTo(_equation[i - 1].Number, _equation[i + 1].Number));
                        break;
                    }
                }
            }

            return _equation[0].Number;
        }
    }

    private bool CanGetNumber(List<GridElement> part, bool isFirstPart)
    {
        if (isFirstPart == true)
        {
            int count = part.Count;

            while (count >= 0)
            {
                for (int i = 0; i < part.Count; i++)
                {
                    if (IsSingleMathSign(isFirstPart, part, i))
                    {
                        if (part[i].MathSign() is Subtraction && i + 1 < part.Count && part[i + 1].IsTakenNumber)
                            count--;
                        else
                        {
                            part.RemoveAt(i);
                            break;
                        }
                    }
                    else
                        count--;
                }

                count--;
            }

            return part.Count > 0 && part[part.Count - 1].IsTakenNumber;
        }   
        else
        {
            int count = part.Count;

            while (count >= 0)
            {
                for (int i = part.Count - 1; i >= 0; i--)
                {
                    if (IsSingleMathSign(isFirstPart, part, i))
                    {
                        part.RemoveAt(i);
                        break;
                    }
                    else
                        count--;
                }

                count--;
            } 

            return part.Count > 0 && part[0].IsTakenNumber || part[0].IsTakenMathSign && part[0].MathSign() is Subtraction;
        }
    }

    private static bool IsSingleMathSign(bool isFirstPart, List<GridElement> part, int i)
    {
        if(isFirstPart)
            return part[i].IsTakenMathSign && i - 1 < 0;
        else
            return part[i].IsTakenMathSign && i + 1 >= part.Count;
    }


    private int CheckPart(List<GridElement> part, int startIndex, int i)
    {
        if (part[i].IsTakenMathSign)
        {
            if (i - 1 >= 0 && part[i - 1].IsTakenNumber)
            {
                _equation.Add(new NumberMathSign(GetNumber(part, startIndex, i)));

                if (IsNextNumber(part, i))
                    _equation.Add(new NumberMathSign(part[i].GridContent as MathSign));
                else
                    part.RemoveAt(i);
            }
            else if(part[i].MathSign() is Subtraction && IsNextNumber(part, i))
                _equation.Add(new NumberMathSign(part[i].GridContent as MathSign));
            else
                part.RemoveAt(i);

            startIndex = i + 1;
        }
        return startIndex;
    }

    private static bool IsNextNumber(List<GridElement> part, int i)
        => i + 1 < part.Count && part[i + 1].IsTakenNumber;

    private int GetNumber(List<GridElement> part, int startIndex, int endIndex)
    {
        int number = 0;
        int multiplier = 1;

        for (int i = endIndex - 1; i >= startIndex; i--)
        {
            number += (part[i].GridContent as Number).Num * multiplier;
            multiplier *= 10;
        }
        return number;
    }

    private bool MathSignAmountZero()
    {
        for(int i = 0; i < _equation.Count; i++)
        {
            if (_equation[i].IsMathSign())
                return false;
        }

        return true;
    }

    private bool HasMultiplicationOrDivision()
    {
        for(int i = 0; i < _equation.Count; i++)
        {
            if(_equation[i].IsMathSign(out MathSign mathSign))
            {
                if (mathSign is Division || mathSign is Multiplication)
                {
                    int number = mathSign.EqualTo(_equation[i - 1].Number, _equation[i + 1].Number);

                    CalculateAndRemove(i, number);

                    return true;
                }
            }
        }

        return false;
    }

    private void CalculateAndRemove(int i, int number)
    {
        _equation[i - 1] = new NumberMathSign(number);
        _equation.RemoveAt(i + 1);
        _equation.RemoveAt(i);
    }
}

public class NumberMathSign
{
    private int? _number;
    private MathSign _mathSign;

    public NumberMathSign(int number) => _number = number;
    public NumberMathSign(MathSign mathSign) => _mathSign = mathSign;

    public int Number => (int)_number;
    public void NegativeNumber() => _number = -_number;

    public bool IsMathSign(out MathSign mathSign)
    {
        mathSign = _mathSign;
        return mathSign != null;
    }
    public bool IsMathSign() => _mathSign != null;
}