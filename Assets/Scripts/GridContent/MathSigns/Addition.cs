using UnityEngine;

[CreateAssetMenu(menuName = "Grid Content/MathSign/Addition")]
public class Addition : MathSign
{
    public override int EqualTo(int firstNumber, int secondNumber) => firstNumber + secondNumber;
}
