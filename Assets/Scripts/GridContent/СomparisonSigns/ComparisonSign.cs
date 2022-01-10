using UnityEngine;

public abstract class ComparisonSign : GridContent
{
    public abstract bool IsTrueExpression(int firstNumber, int secondNumber);
}
