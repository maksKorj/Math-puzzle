using UnityEngine;

public class MainShop : MonoBehaviour
{
    [SerializeField] private GameObject _childe;
    [SerializeField] protected MainWallet _wallet;

    public void OpenShop()
    {
        _childe.SetActive(true);
        _wallet.HideOpenButtons();
    }

    public void CloseShop()
    {
        _childe.SetActive(false);
        _wallet.ShowOpenButtons();
    }
}
