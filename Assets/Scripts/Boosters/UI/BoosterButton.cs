using System;
using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class BoosterButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private BoosterButtonAmount _boosterButtonAmount;
        [SerializeField] private BoosterManager _boosterManager;

        private BoosterItem _boosterItem;
        private Button _button;
        private Action ButtonClick;

        public Type BoosterType => _boosterItem.BoosterType;
        public BoosterItem BoosterItem => _boosterItem;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void AddBooster(BoosterItem boosterItem)
        {
            _boosterItem = boosterItem;
            _boosterItem.Booster.Initialize();
            _image.sprite = _boosterItem.Booster.BoosterImage;

            _boosterButtonAmount.ShowAmount(_boosterItem.Amount);

            ButtonClick = UseBooster;
        }

        public void SetButtonClickToShop() => ButtonClick = OpenShop;

        public void Click() => ButtonClick();

        public void UseBooster()
        {
            _boosterItem.RemoveOne();

            if (_boosterItem.Booster.IsImmediatelyApply == false)
                _boosterManager.SetButtonInteractable(false);

            _boosterButtonAmount.UpdateAmount(_boosterItem.Amount);

            _boosterItem.Booster.ApplyBooster();

            if(_boosterItem.Amount <= 0)
            {
                ButtonClick = OpenShop;
            }
        }

        public void GetBoosterBack()
        {
            _boosterItem.AddAmount();
            _boosterButtonAmount.UpdateAmount(_boosterItem.Amount);

            SetInteractable(true);
        }

        public void SetInteractable(bool interactable) => _button.interactable = interactable;

        public void OpenShop()
        {
            if (_boosterItem != null)
                Debug.Log("Shop");
        }

        public void UpdateAmount()
            => _boosterButtonAmount.UpdateAmount(_boosterItem.Amount);
    }
}

