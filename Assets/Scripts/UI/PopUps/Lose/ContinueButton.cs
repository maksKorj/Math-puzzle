using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private MoveCounter _moveCounter;
    [SerializeField] private int _moveAmount;
    [SerializeField] private int _price;
    [SerializeField] private ContinuePopUp _continuePopUp;

    private MainShop _mainShop;

    public void Buy()
    {
        if(_wallet.CanBuy(_price))
        {
            _moveCounter.AddAmount(_moveAmount);
            _continuePopUp.HideContinueBlock();
        }
        else
        {
            if (_mainShop == null)
                _mainShop = FindObjectOfType<MainShop>();

            if (_wallet is CoinWallet)
                _mainShop.OpenShop();
            else
                _mainShop.OpenGemTab();
        }
    }
    
}