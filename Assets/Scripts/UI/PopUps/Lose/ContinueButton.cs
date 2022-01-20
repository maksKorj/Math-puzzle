using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private MoveCounter _moveCounter;
    [SerializeField] private int _moveAmount;
    [SerializeField] private int _price;
    [SerializeField] private ContinuePopUp _continuePopUp;

    public void Buy()
    {
        if(_wallet.CanBuy(_price))
        {
            _moveCounter.AddAmount(_moveAmount);
            _continuePopUp.HideContinueBlock();
        }
        else
        {
            Debug.Log("Open Shop");
        }
    }
    
}