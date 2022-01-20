using Boosters;
using UnityEngine;

public class BoosterShopPopUp : ShopPopUp
{
    [SerializeField] private BoosterShopButton[] _boosterShopButtons;
    [SerializeField] private FreeBoosterShopButton _freeBoosterShopButton;

    private void Awake()
    {
        for (int i = 0; i < _boosterShopButtons.Length; i++)
            _boosterShopButtons[i].Initialize();

        gameObject.SetActive(false);
    }

    public void OpenShop(BoosterButton button)
    {
        for (int i = 0; i < _boosterShopButtons.Length; i++)
            _boosterShopButtons[i].UpdateButton(button);

        _freeBoosterShopButton.UpdeteUi(button);

        OpenShop();
    }
}
