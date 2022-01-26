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
            for (int i = 0; i < BoosterSaverManager.Instance.SlotItems.Count; i++)
            {
                if(BoosterSaverManager.Instance.SlotItems[i] != null)
                    _boosterButtons[i].AddBooster(BoosterSaverManager.Instance.SlotItems[i]);
                else
                    _boosterButtons[i].SetButtonClickToShop();
            }   

            _unitGrid.OnStartMoving += () => SetButtonInteractable(false);
            _symbolGiverVisual.OnEndGivingSymbol += () => SetButtonInteractable(true);
        }

        public void ReceiveBack(Type boosterType)
        {
            for (int i = 0; i < _boosterButtons.Length; i++)
            {
                if (_boosterButtons[i].BoosterType != null && _boosterButtons[i].BoosterType == boosterType)
                {
                    _boosterButtons[i].GetBoosterBack();
                    break;
                }   
            }

            SetButtonInteractable(true);
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

