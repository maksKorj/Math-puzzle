using Boosters;
using UnityEngine;
using UnityEngine.UI;

public class FreeBoosterShopButton : FreeButton
{
    [SerializeField] private Image _image;
    [SerializeField] private int _boosterAmount;

    private BoosterButton _boosterButton;

    public void UpdeteUi(BoosterButton boosterButton)
    {
        _boosterButton = boosterButton;
        _image.sprite = _boosterButton.BoosterItem.Booster.BoosterImage;
    }

    protected override void Give()
    {
        _boosterButton.BoosterItem.AddAmount(_boosterAmount);
        _boosterButton.UpdateAmount();
    }
}
