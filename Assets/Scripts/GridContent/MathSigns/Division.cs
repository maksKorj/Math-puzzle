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

    public override int EqualTo(out int firstNumber, out int secondNumber, int minSize = -1)
    {
        int equalTo = Random.Range(0, 10);
        
        secondNumber = Random.Range(1, 10);
        firstNumber = equalTo * secondNumber;

        return equalTo;
    }

    public override int EqualTo(int firstNumber, out int secondNumber)
    {
        int equalTo = Random.Range(0, 10);
        secondNumber = firstNumber.GCD(equalTo);

        return equalTo;
    }

    public override int EqualTo(out int firstNumber, int secondNumber)
    {
        if(secondNumber == 0)
        {
            Debug.Break();
            Debug.LogError("Can't be zero!");
        }

        int equalTo = Random.Range(0, 10);
        firstNumber = equalTo * secondNumber;

        return equalTo;
    }
}
