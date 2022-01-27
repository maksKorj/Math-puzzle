using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopButton : MonoBehaviour
{
    [SerializeField] protected Wallet _wallet;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _boosterAmountDisplay;
    [SerializeField] private TextMeshProUGUI _priceDisplay;
    [SerializeField] private TextMeshProUGUI _priceSaleDisplay;
    [SerializeField] private TextMeshProUGUI _saleDisplay;
    [Header("Price/Amount")]
    [SerializeField] private int _boosterAmount;

    private int _price;
    private BoosterButtonElement _boosterButtonElement;
    private Func<int> GetPrice;
    protected MainShop _mainShop;

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
        int price = _boosterButtonElement.BoosterItem.Booster.CoinPrice * _boosterAmount;

        if (_boosterAmount >= 5)
        {
            int totalPrice = Mathf.RoundToInt(price * 0.85f);
            _priceSaleDisplay.text = totalPrice.ToString();
            _saleDisplay.text = $"<s>{price}";

            return totalPrice;
        }

        _priceDisplay.text = price.ToString();
        return price;
    }

    private int GetDiamoundPrice()
    {
        float price = _boosterButtonElement.BoosterItem.Booster.GemPrice;
        int totalPrice;
        
        if (_boosterAmount > 5)
        {
            totalPrice = Mathf.RoundToInt(price * _boosterAmount - price * 5);
            _priceSaleDisplay.text = totalPrice.ToString();
            _saleDisplay.text = $"<s>{price * _boosterAmount}";
            return totalPrice;
        }

        totalPrice = Mathf.RoundToInt(price * _boosterAmount);
        _priceDisplay.text = totalPrice.ToString();
        return totalPrice;
    }

    public void UpdateButton(BoosterButtonElement boosterButtonElement)
    {
        _boosterButtonElement = boosterButtonElement;
        _image.sprite = _boosterButtonElement.BoosterItem.Booster.BoosterImage;
        _price = GetPrice();
    }

    public void Buy()
    {
        if (_wallet.CanBuy(_price))
        {
            _boosterButtonElement.BoosterItem.AddAmount(_boosterAmount);
            _boosterButtonElement.UpdateAmount();
        }
        else
        {
            OpenMainShop();
        }
    }

    protected void OpenMainShop()
    {
        if (_mainShop == null)
            _mainShop = FindObjectOfType<MainShop>();

        if (_wallet is CoinWallet)
            _mainShop.OpenShop();
        else
            _mainShop.OpenGemTab();
    }
}
