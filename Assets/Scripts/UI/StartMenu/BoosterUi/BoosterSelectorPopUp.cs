using UnityEngine;

namespace StartMenu.BoosterUi
{
    public class BoosterSelectorPopUp : MonoBehaviour
    {
        [SerializeField] private BoosterSelectorItem[] _boosterSelectorItems;
        [SerializeField] private PopUpAnimation _popUpAnimation;

        private BoosterButtonSlot _boosterButtonSlot;

        public void Open(BoosterButtonSlot boosterButtonSlot)
        {
            _popUpAnimation.Open();
            _boosterButtonSlot = boosterButtonSlot;

            for (int i = 0; i < _boosterSelectorItems.Length; i++)
                _boosterSelectorItems[i].Hide();

            for (int i = 0; i < BoosterSaverManager.Instance.AvailableBoosterItems.Count; i++)
                _boosterSelectorItems[i].SetItem(BoosterSaverManager.Instance.AvailableBoosterItems[i]);
        }

        public void ChangeBooster(BoosterItem boosterItem)
        {
            BoosterSaverManager.Instance.ChangeSlot(_boosterButtonSlot.ButtonIndex, boosterItem);
            _boosterButtonSlot.SetBooster(boosterItem);

            Close();
        }

        public void Close() => _popUpAnimation.Close();
    }
}

