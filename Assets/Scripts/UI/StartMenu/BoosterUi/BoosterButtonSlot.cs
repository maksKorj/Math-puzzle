using UnityEngine;
using UnityEngine.UI;

namespace StartMenu.BoosterUi
{
    public class BoosterButtonSlot : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private BoosterSelectorPopUp _boosterSelectorPopUp;
        [SerializeField] private LabelNotAvailableAnimation _labelNotAvailableAnimation;

        private int _buttonIndex;
        public int ButtonIndex => _buttonIndex;

        public void SetIndex(int index) => _buttonIndex = index;

        public void SetBooster(BoosterItem boosterItem)
        {
            _image.enabled = true;
            _image.sprite = boosterItem.Booster.BoosterImage;
        }

        public void ResetBooster() => _image.sprite = null;

        public void ButtonClick()
        {
            if(BoosterSaverManager.Instance.AvailableBoosterItems.Count > 0)
            {
                _boosterSelectorPopUp.Open(this);
            }
            else
            {
                _labelNotAvailableAnimation.PlayAnimation();
            }
        }
    }
}

