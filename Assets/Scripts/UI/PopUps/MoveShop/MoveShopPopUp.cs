using UnityEngine;

public class MoveShopPopUp : MonoBehaviour
{
    [SerializeField] private MoveShopButton[] _moveShopButtons;
    [SerializeField] private MainWallet _wallet;

    private void Awake()
    {
        for (int i = 0; i < _moveShopButtons.Length; i++)
            _moveShopButtons[i].Initialize();

        gameObject.SetActive(false);
    }

    public void OpenShop()
    {
        gameObject.SetActive(true);
        _wallet.ShowWallet();
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        _wallet.HideWallet();
    }
}
