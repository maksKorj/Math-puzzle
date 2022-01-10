using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Division")]
public class Division : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber)
    {
        if (secondNumber == 0)
            return -1;
        else
            return firstNumber / secondNumber;
    }
}
