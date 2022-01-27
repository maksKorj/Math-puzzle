using UnityEngine;

public class ShopCoinButton : MonoBehaviour
{
    [SerializeField] private CoinWallet _coinWallet;
    [SerializeField] private DiamondWallet _diamondWallet;
    [SerializeField] private MainShop _mainShop;
    [Header("Properties")]
    [SerializeField] private int _amount;
    [SerializeField] private int _price;

    public void Buy()
    {
        if (_diamondWallet.CanBuy(_price))
            _coinWallet.Add(_amount);
        else
            _mainShop.OpenGemTab();
    }
}
