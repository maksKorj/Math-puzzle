using UnityEngine;

public class FreeContinueButton : FreeMoveShopButton
{
    [SerializeField] private ContinuePopUp _continuePopUp;

    protected override void Give()
    {
        base.Give();
        _continuePopUp.HideContinueBlock();
    }
}
