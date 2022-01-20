using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace StartMenu
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _blockColor = new Color(1, 1, 1, 1);
        [SerializeField] private GameObject _lock;
        [SerializeField] private TextMeshProUGUI _amountDisplay;
        [SerializeField] private GameObject _amountBackground;
        [SerializeField] private InventoryShopPopUp _inventoryShopPopUp;

        private BoosterItem _boosterItem;

        public BoosterItem BoosterItem => _boosterItem;

        public void SetBooster(BoosterItem boosterItem)
        {
            _boosterItem = boosterItem;
            _image.sprite = boosterItem.Booster.BoosterImage;

            if (boosterItem.Booster.IsAvailable(PlayerSaver.LoadPlayerLevel()))
            {
                _amountBackground.SetActive(true);
                _amountDisplay.text = boosterItem.Amount.ToString();
            }
            else
            {
                _lock.SetActive(true);
                _image.color = _blockColor;
            }
        }

        public void OpenShop() => _inventoryShopPopUp.OpenShop(this);

        public void UpdateAmount() => _amountDisplay.text = _boosterItem.Amount.ToString();
    }
}

