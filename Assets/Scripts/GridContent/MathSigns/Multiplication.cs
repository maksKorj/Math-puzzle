using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Multiplication")]
public class Multiplication : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber) => firstNumber * secondNumber;

    public override int EqualTo(out int firstNumber, out int secondNumber, int minSize = -1)
    {
        firstNumber = Random.Range(0, 10);
        secondNumber = Random.Range(0, 10);

        return firstNumber * secondNumber;
    }

    public override int EqualTo(int firstNumber, out int secondNumber)
    {
        secondNumber = Random.Range(0, 10);

        return firstNumber * secondNumber;
    }

    public override int EqualTo(out int firstNumber, int secondNumber)
    {
        firstNumber = Random.Range(0, 10);

        return firstNumber * secondNumber;
    }
}
