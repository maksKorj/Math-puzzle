using DG.Tweening;
using UnityEngine;

public class MainShopPopupAnimation : PopUpAnimation
{
    [SerializeField] private MainWallet _wallet;

    protected override void OpenAmimation()
    {
        _wallet.HideOpenButtons();
        base.OpenAmimation();
    }

    protected override void Disable()
    {
        base.Disable();
        _wallet.ShowOpenButtons();
    }
}
