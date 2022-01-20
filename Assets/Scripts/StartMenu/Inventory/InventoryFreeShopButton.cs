using UnityEngine;
using UnityEngine.UI;

namespace StartMenu
{
    public class InventoryFreeShopButton : FreeButton
    {
        [SerializeField] private Image _image;
        [SerializeField] private int _boosterAmount;

        private InventoryItem _inventoryItem;

        public void UpdeteUi(InventoryItem inventoryItem)
        {
            _inventoryItem = inventoryItem;
            _image.sprite = _inventoryItem.BoosterItem.Booster.BoosterImage;
        }

        protected override void Give()
        {
            _inventoryItem.BoosterItem.AddAmount(_boosterAmount);
            _inventoryItem.UpdateAmount();
        }
    }
}

