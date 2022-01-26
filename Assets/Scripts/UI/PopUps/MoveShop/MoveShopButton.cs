using UnityEngine;
using TMPro;

public class MoveShopButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _moveAmountDisplay;
    [SerializeField] private TextMeshProUGUI _priceDisplay;
    [SerializeField] private MoveCounter _moveCounter;

    [Header("Price/Amount")]
    [SerializeField] private int _price;
    [SerializeField] private int _moveAmount;

    private MainShop _mainShop;

    public void Initialize()
    {
        _moveAmountDisplay.text = $"+ {_moveAmount} moves";
        _priceDisplay.text = _price.ToString();
    }

    public void Buy()
    {
        if (_wallet.CanBuy(_price))
            _moveCounter.AddAmount(_moveAmount);
        else
            OpenMainShop();
    }

    private void OpenMainShop()
    {
        if (_mainShop == null)
            _mainShop = FindObjectOfType<MainShop>();

        if (_wallet is CoinWallet)
            _mainShop.OpenShop();
        else
            _mainShop.OpenGemTab();
    }
}
