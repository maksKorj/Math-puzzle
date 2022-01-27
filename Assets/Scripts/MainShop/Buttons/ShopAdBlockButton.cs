using UnityEngine;
using UnityEngine.Purchasing;

public class ShopAdBlockButton : MonoBehaviour
{
    [SerializeField] private string _id = "com.maks_kor.math_puzzle.adb_block";

    public void OnPuchaseComplete(Product product)
    {
        if (product.definition.id == _id)
        {
            Buy();
            Debug.Log(_id + " is purchasesd");
        }
    }

    public void Buy()
    {
        StateSaver.TurnOnAdblock();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log(product.definition.id + purchaseFailureReason);
    }
}
