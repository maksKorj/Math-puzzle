using StartMenu;
using UnityEngine;
using UnityEngine.Purchasing;

public class ShopInfiniteLivesButton : MonoBehaviour
{
    [SerializeField] private Lives _lives;
    [SerializeField] private string _id = "com.maks_kor.math_puzzle.infinite.lives";

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
        StateSaver.SetInfiniteLives();
        _lives.ShowIniniteSign();   
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
    {
        Debug.Log(product.definition.id + purchaseFailureReason);
    }

}
