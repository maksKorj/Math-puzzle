using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StartMenu.Inventory
{
    public class InventoryItem : BoosterButtonElement
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _blockColor = new Color(1, 1, 1, 1);
        [SerializeField] private GameObject _lock;
        [SerializeField] private TextMeshProUGUI _amountDisplay;
        [SerializeField] private GameObject _amountBackground;
        [SerializeField] private InventoryShopPopUp _inventoryShopPopUp;
        [SerializeField] private InventoryLabelAnimation _inventoryLabelAnimation;

        public void SetBooster(BoosterItem boosterItem)
        {
            BoosterItem = boosterItem;
            _image.sprite = boosterItem.Booster.BoosterImage;

            if (boosterItem.Booster.IsAvailable(PlayerSaver.LoadPlayerLevel()))
            {
                _amountBackground.SetActive(true);
                _amountDisplay.text = boosterItem.Amount.ToString();
            }
            else
            {
                _lock.SetActive(true);
                _inventoryLabelAnimation.UpdateLevel(BoosterItem.Booster.AvailableFromLevel);
                _image.color = _blockColor;
            }
        }

        public void OpenShop() => _inventoryShopPopUp.OpenShop(this);

        public override void UpdateAmount() => _amountDisplay.text = BoosterItem.Amount.ToString();
    }
}

