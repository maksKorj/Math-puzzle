using UnityEngine;

[CreateAssetMenu(menuName = "VictoryCondition/RemoveChipAmount")]
public class RemoveChipAmount : VictoryCondition
{
    [SerializeField] private int _amountToWin;

    public override void ShowUI()
        => FindObjectOfType<RemoveChipAmountHandler>().Initialize(_amountToWin);
}
