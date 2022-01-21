using UnityEngine;
using UnityEngine.UI;

public class FreeBoosterShopButton : FreeButton
{
    [SerializeField] private Image _image;
    [SerializeField] private int _boosterAmount;

    private BoosterButtonElement _boosterButtonElement;

    public void UpdeteUi(BoosterButtonElement boosterButtonElement)
    {
        _boosterButtonElement = boosterButtonElement;
        _image.sprite = _boosterButtonElement.BoosterItem.Booster.BoosterImage;
    }

    protected override void Give()
    {
        _boosterButtonElement.BoosterItem.AddAmount(_boosterAmount);
        _boosterButtonElement.UpdateAmount();
    }
}
