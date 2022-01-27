using StartMenu.Inventory;

public class BoosterShopPopUp : InventoryShopPopUp
{
    protected override void CloseShop()
    {
        (_popUpAnimation as PopUpAnimationAditional).Close();
    }

    protected override void OpenShop()
    {
        (_popUpAnimation as PopUpAnimationAditional).Open();
    }
}
