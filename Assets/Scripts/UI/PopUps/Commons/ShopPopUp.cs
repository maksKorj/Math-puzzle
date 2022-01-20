using UnityEngine;

public abstract class ShopPopUp : MonoBehaviour
{
    [SerializeField] private GameObject _wallet;

    public virtual void OpenShop() => SetActiveObject(true);
    public void CloseShop() => SetActiveObject(false);

    private void SetActiveObject(bool isActive)
    {
        gameObject.SetActive(isActive);
        _wallet.SetActive(isActive);
    }
}
