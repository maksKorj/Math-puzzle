using DG.Tweening;
using UnityEngine;

public class PopUpAnimationAditional : PopUpAnimation
{
    [SerializeField] private MainWallet _wallet;

    protected override void OpenAmimation()
    {
        _background.DOFade(0.8f, 0.4f).SetEase(Ease.InCubic);
        _rectTransform.DOScale(Vector2.one, 0.5f).SetEase(Ease.InCubic).OnComplete(() => _wallet.ShowWallet());
    }

    protected override void CloseAnimation()
    {
        _wallet.HideWallet();
        base.CloseAnimation();
    }
}
