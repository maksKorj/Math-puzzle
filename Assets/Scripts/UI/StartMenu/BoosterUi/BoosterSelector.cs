using UnityEngine;
using TMPro;

namespace StartMenu.BoosterUi
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

            if (BoosterSaverManager.Instance.SlotItems.Count > 0)
            {
                for (int i = 0; i < BoosterSaverManager.Instance.SlotItems.Count; i++)
                {
                    if(BoosterSaverManager.Instance.SlotItems[i] != null)
                        _boosterButtonSlots[i].SetBooster(BoosterSaverManager.Instance.SlotItems[i]);
                    else
                        _boosterButtonSlots[i].ResetBooster();
                }
            }
        }

        public void Close()
        {
            _childe.SetActive(false);
        }
    }
}

