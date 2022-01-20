using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Boosters;

public class BoosterShopButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _boosterAmountDisplay;
    [SerializeField] private TextMeshProUGUI _priceDisplay;
    [Header("Price/Amount")]
    [SerializeField] private int _boosterAmount;

    private int _price;
    private BoosterButton _boosterButton;
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
        int price = _boosterButton.BoosterItem.Booster.CoinPrice * _boosterAmount;

        if(_boosterAmount >= 5)
        {
            return Mathf.RoundToInt(price * 0.85f);
        }

        return price * _boosterAmount;
    }
    private int GetDiamoundPrice()
    {
        int price = _boosterButton.BoosterItem.Booster.GemPrice;

        if(_boosterAmount > 3)
        {
            return price * 2;
        }

        return price;
    }

    public void UpdateButton(BoosterButton boosterButton)
    {
        _boosterButton = boosterButton;
        _image.sprite = _boosterButton.BoosterItem.Booster.BoosterImage;
        _price = GetPrice();

        _priceDisplay.text = _price.ToString();
    }

    public void Buy()
    {
        if(_wallet.CanBuy(_price))
        {
            _boosterButton.BoosterItem.AddAmount(_boosterAmount);
            _boosterButton.UpdateAmount();
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
