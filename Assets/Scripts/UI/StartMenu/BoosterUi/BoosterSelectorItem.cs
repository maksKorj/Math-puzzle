using UnityEngine;
using UnityEngine.UI;

namespace StartMenu.BoosterUi
{
    public class BoosterSelectorItem : MonoBehaviour
    {
        [SerializeField] private BoosterSelectorPopUp _boosterSelectorPopUp;

        private BoosterItem _boosterItem;
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void Hide() => gameObject.SetActive(false);

        public void SetItem(BoosterItem boosterItem)
        {
            gameObject.SetActive(true);
            _boosterItem = boosterItem;

            _image.sprite = _boosterItem.Booster.BoosterImage;
        }

        public void ChangeItem()
            => _boosterSelectorPopUp.ChangeBooster(_boosterItem);
    }
}


