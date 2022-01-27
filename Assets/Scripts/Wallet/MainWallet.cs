using UnityEngine;

public class MainWallet : MonoBehaviour
{
    [SerializeField] private CoinWallet _coinWallet;
    [SerializeField] private DiamondWallet _diamondWallet;

    public void ShowWallet()
    {
        gameObject.SetActive(true);
    }

    public void HideWallet()
    {
        gameObject.SetActive(false);
    }

    public void ShowOpenButtons()
    {
        _coinWallet.ShowOpenButton();
        _diamondWallet.ShowOpenButton();
    }

    public void HideOpenButtons()
    {
        _coinWallet.HideOpenButton();
        _diamondWallet.HideOpenButton();
    }
}
