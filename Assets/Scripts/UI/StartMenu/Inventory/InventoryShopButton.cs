using UnityEngine;

namespace StartMenu.Inventory
{
    public class InventoryShopButton : ShopButton
    {
        protected override void OpenMainShop()
        {
            if (_mainShop == null)
                _mainShop = FindObjectOfType<StartMainShop>();

            _mainShop.OpenShop();
        }
    }
}

