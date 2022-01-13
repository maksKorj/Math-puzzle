using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/Comparison/LessThan")]
public class LessThanSign : ComparisonSign
{
    [SerializeField] private bool _cabBeEqual;

    public override bool IsTrueExpression(int firstNumber, int secondNumber)
        => _cabBeEqual ? firstNumber <= secondNumber : firstNumber < secondNumber;

    public override bool CanCorrecting(int leftPart, int rightPart, out int correctingRightPart)
    {
        if (_cabBeEqual)
        {
            correctingRightPart = Random.Range(leftPart, 100);
            return true;
        }
        else
        {
            if (rightPart == 99)
            {
                correctingRightPart = -1;
                return false;
            }
            else
            {
                correctingRightPart = Random.Range(leftPart + 1, 100);
                return true;
            }
        }
    }
}
