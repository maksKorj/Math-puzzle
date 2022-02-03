using StartMenu.Inventory;

public class BoosterShopPopUp : InventoryShopPopUp
{
    protected override void OpenShop()
    {
        (_popUpAnimation as PopUpAnimationAditional).Open();

        BackButton.Instance.AddBackButtonAction(ClosePopUp);
    }

    protected override void CloseShop()
    {
        ClosePopUp();
        BackButton.Instance.RemoveLastAction();
    }

    private void ClosePopUp()
    {
        (_popUpAnimation as PopUpAnimationAditional).Close();

        StopLabelAnimation();
    }
}
