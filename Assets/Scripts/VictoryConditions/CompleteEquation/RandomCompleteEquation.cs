using UnityEngine;

[CreateAssetMenu(menuName = "VictoryCondition/RandomCompleteEquation")]
public class RandomCompleteEquation : RandomVictoryCondition
{
    public override void SetValue(int level)
    {
        _amountToWin = Random.Range(4, GetMax(level));

        int moves = Mathf.RoundToInt(_amountToWin * 4f);
        _moveAmount = Random.Range(moves, moves + 15);
    }

    private int GetMax(int level)
    {
        int amount = 3 + level / 2;
        amount = Mathf.Max(amount, 5);
        amount = Mathf.Min(amount, 25);

        return amount + 1;
    }

    public override void ShowUI()
        => FindObjectOfType<CompleteEquationHandler>().Initialize(_amountToWin);
}
