using UnityEngine;
using TMPro;

namespace StartMenu
{
    public class BoosterSelector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelName;
        [SerializeField] private GameObject _childe;
        [SerializeField] private BoosterButtonSlot[] _boosterButtonSlots;

        private void Awake()
        {
            for (int i = 0; i < _boosterButtonSlots.Length; i++)
                _boosterButtonSlots[i].SetIndex(i);
        }

        public void Open(string level)
        {
            _childe.SetActive(true);
            _levelName.text = level;

            if (BoosterSaverManager.Instance.TakenSlotItems.Count > 0)
            {
                for (int i = 0; i < BoosterSaverManager.Instance.TakenSlotItems.Count; i++)
                    _boosterButtonSlots[i].SetBooster(BoosterSaverManager.Instance.TakenSlotItems[i]);
            }
        }

        public void Close()
        {
            _childe.SetActive(false);
        }
    }
}

