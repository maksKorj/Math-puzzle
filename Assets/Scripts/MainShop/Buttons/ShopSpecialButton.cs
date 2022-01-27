using UnityEngine;
using UnityEngine.Purchasing;

public class ShopSpecialButton : MonoBehaviour
{
    [SerializeField] private ShopInfiniteLivesButton _shopInfiniteLivesButton;
    [SerializeField] private ShopAdBlockButton _shopAdBlockButton;
    [SerializeField] private string _id = "com.maks_kor.math_puzzle.special_offer";

    public void OnPuchaseComplete(Product product)
    {
        if (product.definition.id == _id)
        {
            _shopInfiniteLivesButton.Buy();
            _shopAdBlockButton.Buy();
            Debug.Log(_id + " is purchasesd");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log(product.definition.id + purchaseFailureReason);
    }
}
