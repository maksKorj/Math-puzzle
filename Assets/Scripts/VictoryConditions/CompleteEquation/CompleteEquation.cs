using UnityEngine;

[CreateAssetMenu(menuName = "VictoryCondition/CompleteEquation")]
public class CompleteEquation : VictoryCondition
{
    [SerializeField] protected int _amountToWin;

    public override void ShowUI()
        => FindObjectOfType<CompleteEquationHandler>().Initialize(_amountToWin);
}
