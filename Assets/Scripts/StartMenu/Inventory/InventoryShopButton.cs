using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StartMenu
{
    public class InventoryShopButton : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _boosterAmountDisplay;
        [SerializeField] private TextMeshProUGUI _priceDisplay;
        [Header("Price/Amount")]
        [SerializeField] private int _boosterAmount;

        private int _price;
        private InventoryItem _inventoryItem;
        private Func<int> GetPrice;
        private MainShop _mainShop;

        public void Initialize()
        {
            _boosterAmountDisplay.text = $"x{_boosterAmount}";

            if (_wallet is CoinWallet)
                GetPrice = GetCoinPrice;
            else
                GetPrice = GetDiamoundPrice;
        }

        private int GetCoinPrice()
        {
            int price = _inventoryItem.BoosterItem.Booster.CoinPrice * _boosterAmount;

            if (_boosterAmount >= 5)
            {
                return Mathf.RoundToInt(price * 0.85f);
            }

            return price * _boosterAmount;
        }

        private int GetDiamoundPrice()
        {
            int price = _inventoryItem.BoosterItem.Booster.GemPrice;

            if (_boosterAmount > 3)
            {
                return price * 2;
            }

            return price;
        }

        public void UpdateButton(InventoryItem inventoryItem)
        {
            _inventoryItem = inventoryItem;
            _image.sprite = _inventoryItem.BoosterItem.Booster.BoosterImage;
            _price = GetPrice();

            _priceDisplay.text = _price.ToString();
        }

        public void Buy()
        {
            if (_wallet.CanBuy(_price))
            {
                _inventoryItem.BoosterItem.AddAmount(_boosterAmount);
                _inventoryItem.UpdateAmount();
            }
            else
            {
                OpenMainShop();
            }
        }

        private void OpenMainShop()
        {
            if (_mainShop == null)
                _mainShop = FindObjectOfType<MainShop>();

            _mainShop.OpenShop();
        }
    }
}

