using StartMenu.Inventory;
using UnityEngine;

public class BoosterShopPopUp : InventoryShopPopUp
{
    [SerializeField] private MainWallet _wallet;

    protected override void CloseShop()
    {
        base.CloseShop();
        _wallet.HideWallet();
    }

    protected override void OpenShop()
    {
        base.OpenShop();
        _wallet.ShowWallet();
        
    }
}
