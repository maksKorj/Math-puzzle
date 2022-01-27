using UnityEngine;
using TMPro;

namespace StartMenu.Inventory
{
    public class InventoryShopPopUp : MonoBehaviour
    {
        [SerializeField] private ShopButton[] _boosterShopButtons;
        [SerializeField] private FreeBoosterShopButton _freeBoosterShopButton;
        [SerializeField] private TextMeshProUGUI _boosterNameDisplay;
        [SerializeField] protected PopUpAnimation _popUpAnimation;

        private void Awake()
        {
            for (int i = 0; i < _boosterShopButtons.Length; i++)
                _boosterShopButtons[i].Initialize();
        }

        public void OpenShop(BoosterButtonElement boosterButtonElement)
        {
            OpenShop();

            for (int i = 0; i < _boosterShopButtons.Length; i++)
                _boosterShopButtons[i].UpdateButton(boosterButtonElement);

            _freeBoosterShopButton.UpdeteUi(boosterButtonElement);

            _boosterNameDisplay.text = boosterButtonElement.BoosterItem.Booster.Name;
        }

        public void Close() => CloseShop();

        protected virtual void OpenShop()
        {
            _popUpAnimation.Open();
        }

        protected virtual void CloseShop()
        {
            _popUpAnimation.Close();
        }
    }
}

