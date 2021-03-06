using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Addition")]
public class Addition : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber) => firstNumber + secondNumber;

    public override int EqualTo(out int firstNumber, out int secondNumber, int minSize = -1)
    {
        if(minSize > 0)
        {
            firstNumber = (minSize >= 8) ? Random.Range(0, 100) : Random.Range(0, 10);
            secondNumber = (minSize >= 8 && firstNumber < 10) ? Random.Range(0, 100) : Random.Range(0, 10);
        }
        else
        {
            firstNumber = Random.Range(0, 10);
            secondNumber = Random.Range(0, 10);
        }
        

        if(firstNumber + secondNumber >= 100)
        {
            secondNumber = Mathf.Max(firstNumber, secondNumber);
            firstNumber = Random.Range(secondNumber, 100) - secondNumber;
        }

        return firstNumber + secondNumber;
    }

    public override int EqualTo(int firstNumber, out int secondNumber)
    {
        secondNumber = Random.Range(0, 10);
        return firstNumber + secondNumber;
    }

    public override int EqualTo(out int firstNumber, int secondNumber)
    {
        firstNumber = Random.Range(0, 10);
        return firstNumber + secondNumber;
    }
}
