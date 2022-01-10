using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Multiplication")]
public class Multiplication : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber) => firstNumber * secondNumber;
}
