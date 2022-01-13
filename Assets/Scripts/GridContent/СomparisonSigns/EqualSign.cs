using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/Comparison/Equal")]
public class EqualSign : ComparisonSign
{
    public override bool IsTrueExpression(int firstNumber, int secondNumber)
        => firstNumber == secondNumber;

    public override bool CanCorrecting(int leftPart, int rightPart, out int correctingRightPart)
    {
        correctingRightPart = rightPart;
        return true;
    }
}
