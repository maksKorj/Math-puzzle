using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Subtraction")]
public class Subtraction : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber) => firstNumber - secondNumber;
}
