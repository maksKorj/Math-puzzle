using UnityEngine;
using UnityEngine.UI;

namespace StartMenu
{
    public class BoosterButtonSlot : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private BoosterSelectorPopUp _boosterSelectorPopUp;

        private int _buttonIndex;
        public int ButtonIndex => _buttonIndex;

        public void SetIndex(int index) => _buttonIndex = index;

        public void SetBooster(BoosterItem boosterItem)
        {
            _image.sprite = boosterItem.Booster.BoosterImage;
        }

        public void ButtonClick()
        {
            if(BoosterSaverManager.Instance.AvailableBoosterItems.Count > 0)
            {
                _boosterSelectorPopUp.Open(this);
            }
        }
    }
}

