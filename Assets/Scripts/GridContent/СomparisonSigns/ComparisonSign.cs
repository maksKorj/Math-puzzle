using UnityEngine;

public abstract class ComparisonSign : GridContent
{
    public abstract bool IsTrueExpression(int firstNumber, int secondNumber);

    public abstract bool CanCorrecting(int leftPart, int rightPart, out int correctingRightPart);
}
