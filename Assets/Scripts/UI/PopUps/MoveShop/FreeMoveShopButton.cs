using UnityEngine;

public class FreeMoveShopButton : FreeButton
{
    [SerializeField] private MoveCounter _moveCounter;
    [SerializeField] private int _moveAmount;

    protected override void Give() => _moveCounter.AddAmount(_moveAmount);
}
