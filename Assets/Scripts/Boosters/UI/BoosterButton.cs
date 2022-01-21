using System;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class BoosterButton : BoosterButtonElement
    {
        [SerializeField] private Image _image;
        [SerializeField] private BoosterButtonAmount _boosterButtonAmount;
        [SerializeField] private BoosterManager _boosterManager;

        private Button _button;
        private Action ButtonClick;
        private BoosterShopPopUp _boosterShopPopUp;

        public Type BoosterType => BoosterItem.BoosterType;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void AddBooster(BoosterItem boosterItem)
        {
            BoosterItem = boosterItem;
            BoosterItem.Booster.Initialize();
            _image.sprite = BoosterItem.Booster.BoosterImage;

            _boosterButtonAmount.ShowAmount(BoosterItem.Amount);

            if (BoosterItem.Amount > 0)
                ButtonClick = UseBooster;
            else
                ButtonClick = OpenShop;
        }

        public void SetButtonClickToShop() => ButtonClick = OpenShop;

        public void Click() => ButtonClick();

        public void UseBooster()
        {
            BoosterItem.RemoveOne();

            if (BoosterItem.Booster.IsImmediatelyApply == false)
                _boosterManager.SetButtonInteractable(false);

            _boosterButtonAmount.UpdateAmount(BoosterItem.Amount);

            BoosterItem.Booster.ApplyBooster();

            if(BoosterItem.Amount <= 0)
            {
                ButtonClick = OpenShop;
            }
        }

        public void GetBoosterBack()
        {
            BoosterItem.AddAmount();
            _boosterButtonAmount.UpdateAmount(BoosterItem.Amount);
        }

        public void SetInteractable(bool interactable) => _button.interactable = interactable;

        public void OpenShop()
        {
            if (BoosterItem != null)
            {
                if (_boosterShopPopUp == null)
                    _boosterShopPopUp = FindObjectOfType<BoosterShopPopUp>();

                _boosterShopPopUp.OpenShop(this);
            }
        }

        public override void UpdateAmount()
        {
            _boosterButtonAmount.UpdateAmount(BoosterItem.Amount);

            if (BoosterItem.Amount > 0)
                ButtonClick = UseBooster;
            else
                ButtonClick = OpenShop;
        }
    }
}

