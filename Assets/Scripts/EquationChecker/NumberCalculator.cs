using LevelBuilder;
using System.Collections.Generic;
using UnityEngine;

public class NumberCalculator
{
    public int GetNumber(List<GridElement> part, bool isFirstPart)
    {
        int? number = null;
        int startIndex = 0;

        var numbers = new List<int>();
        var mathSigns = new List<MathSign>();

        for (int i = 0; i < part.Count; i++)
        {
            if(isFirstPart)
                startIndex = CheckLeftMathSign(part, startIndex, numbers, mathSigns, i);
            else
                startIndex = CheckRightMathSign(part, startIndex, numbers, mathSigns, i);
        }

        if (mathSigns.Count > 0 && startIndex < part.Count)
        {
            numbers.Add(GetNumber(part, startIndex, part.Count));
        }

        if (mathSigns.Count == 0)
        {
            return GetNumber(part, 0, part.Count);
        }
        else
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i == 1)
                    continue;

                if (number == null)
                {
                    if (i + 1 < numbers.Count)
                    {
                        number = mathSigns[i].EqualTo(numbers[i], numbers[i + 1]);
                    }
                    else
                    {
                        number = numbers[i];
                        break;
                    }
                }
                else
                {
                    if (i - 1 < mathSigns.Count)
                    {
                        number = mathSigns[i - 1].EqualTo((int)number, numbers[i]);
                    }
                    else
                        break;
                }
            }
        }

        return (int)number;
    }

    private int CheckLeftMathSign(List<GridElement> part, int startIndex, List<int> numbers, List<MathSign> mathSigns, int i)
    {
        if (part[i].IsTakenMathSign)
        {
            if (i - 1 >= 0 && part[i - 1] != null && part[i - 1].IsTakenNumber)
            {
                numbers.Add(GetNumber(part, startIndex, i));
                mathSigns.Add(part[i].GridContent as MathSign);

                startIndex = i + 1;
            }
            else
            {
                part.RemoveAt(i);
                startIndex = i + 1;
            }
        }

        return startIndex;
    }

    private int CheckRightMathSign(List<GridElement> part, int startIndex, List<int> numbers, List<MathSign> mathSigns, int i)
    {
        if (part[i].IsTakenMathSign)
        {
            if (i + 1 < part.Count && part[i + 1] != null && part[i + 1].IsTakenNumber)
            {
                numbers.Add(GetNumber(part, startIndex, i));
                mathSigns.Add(part[i].GridContent as MathSign);

                startIndex = i + 1;
            }
            else
            {
                part.RemoveAt(i);
                startIndex = i + 1;
            }
        }

        return startIndex;
    }

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
}
