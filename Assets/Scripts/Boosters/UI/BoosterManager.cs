using System;
using UnityEngine;

namespace Boosters
{
    public class BoosterManager : MonoBehaviour
    {
        [SerializeField] private BoosterButton[] _boosterButtons;
        [SerializeField] private MainScreen[] _guideScreens;
        [SerializeField] private SymbolGiverVisual _symbolGiverVisual;
        [SerializeField] private UnitGrid _unitGrid;

        private void Start()
        {
            if(BoosterSaverManager.Instance.TakenSlotItems.Count > 0)
            {
                for (int i = 0; i < BoosterSaverManager.Instance.TakenSlotItems.Count; i++)
                    _boosterButtons[i].AddBooster(BoosterSaverManager.Instance.TakenSlotItems[i]);
            }

            int startIndex = Mathf.Max(0, BoosterSaverManager.Instance.TakenSlotItems.Count - 1);
            for(int i = startIndex; i < _boosterButtons.Length; i++)
                _boosterButtons[i].SetButtonClickToShop();

            _unitGrid.OnStartMoving += () => SetButtonInteractable(false);
            _symbolGiverVisual.OnEndGivingSymbol += () => SetButtonInteractable(true);
        }

        public void ReceiveBack(Type boosterType)
        {
            for (int i = 0; i < _boosterButtons.Length; i++)
            {
                if (_boosterButtons[i].BoosterType == boosterType)
                    _boosterButtons[i].GetBoosterBack();
            }
        }

        public MainScreen GetGuideScreen(Type boosterType)
        {
            for(int i = 0; i < _guideScreens.Length; i++)
            {
                if (_guideScreens[i].GetType() == boosterType)
                    return _guideScreens[i];
            }

            Debug.LogError("Guide Screne isn't found! Add to array");
            Debug.Break();
            return null;
        }

        public void SetButtonInteractable(bool interactable)
        {
            for(int i = 0; i < _boosterButtons.Length; i++)
                _boosterButtons[i].SetInteractable(interactable);
        }
    }
}

