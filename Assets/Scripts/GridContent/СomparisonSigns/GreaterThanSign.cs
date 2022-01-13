using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/Comparison/GreaterThan")]
public class GreaterThanSign : ComparisonSign
{
    [SerializeField] private bool _cabBeEqual;

    public override bool IsTrueExpression(int firstNumber, int secondNumber)
        => _cabBeEqual ? firstNumber >= secondNumber : firstNumber > secondNumber;

    public override bool CanCorrecting(int leftPart, int rightPart, out int correctingRightPart)
    {
        if(_cabBeEqual)
        {
            correctingRightPart = Random.Range(0, leftPart + 1);
            return true;
        }
        else
        {
            if (rightPart == 0)
            {
                correctingRightPart = -1;
                return false;
            }  
            else
            {
                correctingRightPart = Random.Range(0, leftPart);
                return true;
            }
        }
    }
}
