using UnityEngine;
using TMPro;

namespace StartMenu.BoosterUi
{
    public class BoosterSelector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelName;
        [SerializeField] private PopUpAnimation _popupAnimation;
        [SerializeField] private BoosterButtonSlot[] _boosterButtonSlots;

        private void Awake()
        {
            for (int i = 0; i < _boosterButtonSlots.Length; i++)
                _boosterButtonSlots[i].SetIndex(i);
        }

        public void Open(string level)
        {
            _popupAnimation.Open();
            _levelName.text = level;

            BoosterSaverManager.Instance.SetBoosterSlots();

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
            _popupAnimation.Close();
        }
    }
}

