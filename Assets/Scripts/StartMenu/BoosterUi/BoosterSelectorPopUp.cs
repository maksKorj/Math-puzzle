using UnityEngine;

namespace StartMenu
{
    public class BoosterSelectorPopUp : MonoBehaviour
    {
        [SerializeField] private BoosterSelectorItem[] _boosterSelectorItems;

        private BoosterButtonSlot _boosterButtonSlot;

        public void Open(BoosterButtonSlot boosterButtonSlot)
        {
            gameObject.SetActive(true);
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

        public void Close() => gameObject.SetActive(false);
    }
}

