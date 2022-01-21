
public class BoosterShopButton : ShopButton
{
    protected override void OpenMainShop()
    {
        if (_mainShop == null)
            _mainShop = FindObjectOfType<MainShop>();

        _mainShop.OpenShop();
    }
}
