using LevelBuilder;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boosters
{
    public class ChangeChipHandler : MainScreen
    {
        [SerializeField] private SelectSymbolScreen _selectSymbolScreen;

        private GridElement _currentGridElement;
        private WaitForSeconds _delay;

        protected override bool IsCorrectRaycastResult(RaycastResult result)
        {
            if (result.gameObject.TryGetComponent(out GridElement gridElement))
            {
                _currentGridElement = gridElement;
                ShowSymbolSelectScreen();
                return true;
            }

            return false;
        }

        private void ShowSymbolSelectScreen()
        {
            _mainScreenProperty.GridAnimation.UnSelectAll();
            HidePanel();
            _selectSymbolScreen.ShowWindow(SetContent, HideAndGiveBack);
        }

        public void SetContent(GridContent gridContent)
            => StartCoroutine(WaitAndChange(gridContent));

        private IEnumerator WaitAndChange(GridContent gridContent)
        {
            if (_currentGridElement.IsTaken)
            {
                _currentGridElement.PlayHideEffect();
                yield return new WaitForSeconds(_currentGridElement.HidingTime);
                ShowElement(gridContent);
            }
            else
            {
                ShowElement(gridContent);
            }
        }

        private void ShowElement(GridContent gridContent)
        {
            _currentGridElement.SetContent(gridContent);
            _currentGridElement.ScaleToFullSize();

            CheckDelay();

            StartCoroutine(WaitAndCheckEquation(_delay));
        }

        private void CheckDelay()
        {
            if (_delay == null)
                _delay = new WaitForSeconds(_currentGridElement.ShowingTime);
        }
    }
}


