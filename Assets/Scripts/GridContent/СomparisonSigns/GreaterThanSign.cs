using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/Comparison/GreaterThan")]
public class GreaterThanSign : ComparisonSign
{
    [SerializeField] private bool _cabBeEqual;

    public override bool IsTrueExpression(int firstNumber, int secondNumber)
        => _cabBeEqual ? firstNumber >= secondNumber : firstNumber > secondNumber;
}
