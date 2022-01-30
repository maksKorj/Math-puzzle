using UnityEngine;

[CreateAssetMenu(menuName = "VictoryCondition/RandomRemoveChipAmount")]
public class RandomRemoveChipAmount : RandomVictoryCondition
{
    public override void SetValue(int level)
    {
        _amountToWin = Random.Range(15, GetMax(level));
        _moveAmount = Random.Range(_amountToWin, _amountToWin + 15);
    }

    private int GetMax(int level)
    {
        int amount = 15 + level / 2;
        amount = Mathf.Max(amount, 20);
        amount = Mathf.Min(amount, 55);

        return amount + 1;
    }

    public override void ShowUI()
        => FindObjectOfType<RemoveChipAmountHandler>().Initialize(_amountToWin);
}
