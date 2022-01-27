using UnityEngine;
using UnityEngine.Purchasing;

public class ShopGemButton : MonoBehaviour
{
    [SerializeField] private DiamondWallet _diamondWallet;
    [SerializeField] private int _amount;

    private string _startId = "com.maks_kor.math_puzzle.diamonds_";

    public void OnPuchaseComplete(Product product)
    {
        var id = _startId + _amount.ToString();

        if (product.definition.id == id)
        {
            _diamondWallet.Add(_amount);
            Debug.Log(_startId + " is purchasesd");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log(product.definition.id + purchaseFailureReason);
    }
}
