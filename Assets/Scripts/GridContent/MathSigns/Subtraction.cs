using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Subtraction")]
public class Subtraction : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber) => firstNumber - secondNumber;

    public override int EqualTo(out int firstNumber, out int secondNumber, int minSize = -1)
    {
        if(minSize > 0)
            firstNumber = (minSize >= 8) ? Random.Range(0, 100) : Random.Range(0, 10);
        else
            firstNumber = Random.Range(0, 10);

        secondNumber = Mathf.Min(Random.Range(0, 10), firstNumber);

        if (secondNumber == firstNumber)
            secondNumber = Random.Range(0, firstNumber + 1);

        return firstNumber - secondNumber;
    }

    public override int EqualTo(int firstNumber, out int secondNumber)
    {
        secondNumber = Mathf.Min(Random.Range(0, 10), firstNumber);

        if (secondNumber == firstNumber)
            secondNumber = Random.Range(0, firstNumber + 1);

        return firstNumber - secondNumber;
    }

    public override int EqualTo(out int firstNumber, int secondNumber)
    {
        firstNumber = Random.Range(secondNumber, 10);

        return firstNumber - secondNumber;
    }
}
