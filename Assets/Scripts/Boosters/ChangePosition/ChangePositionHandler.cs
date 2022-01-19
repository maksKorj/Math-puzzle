using LevelBuilder;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boosters
{
    public class ChangePositionHandler : MainScreen
    {
        private GridElement _currentGridElement;
        private Coroutine _coroutine;
        private WaitForSeconds _showDelay, _selectDelay, _hideDelay;

        public override void Apply()
        {
            base.Apply();
            _text.text = "Choose chip to move.";
        }

        protected override bool IsCorrectRaycastResult(RaycastResult result)
        {
            if (_currentGridElement == null)
            {
                if (result.gameObject.TryGetComponent(out GridElement gridElement) && gridElement.IsTaken)
                {
                    SelectChipToMove(gridElement);
                    return false;
                }
            }
            else
            {
                if (result.gameObject.TryGetComponent(out GridElement gridElement) && gridElement.IsTaken == false)
                {
                    MoveTo(gridElement);
                    return true;
                }
            }

            return false;
        }

        private void SelectChipToMove(GridElement gridElement)
        {
            _canClick = false;

            _mainScreenProperty.GridAnimation.UnSelectAll();
            gridElement.SelectChangePosition();

            StartCoroutine(WaitAndDisplay(gridElement));
        }

        private IEnumerator WaitAndDisplay(GridElement gridElement)
        {
            if (_selectDelay == null)
                _selectDelay = new WaitForSeconds(gridElement.ChangePozitionEffectTime);
            yield return _selectDelay;

            _currentGridElement = gridElement;
            _text.text = "Choose empty cell to move";
            _canClick = true;
        }

        private void MoveTo(GridElement gridElement)
        {
            _canClick = false;

            gridElement.SetContent(_currentGridElement.GridContent);
            _currentGridElement.PlayHideEffect();

            if (_coroutine == null)
                _coroutine = StartCoroutine(WaitAndShowInNewPosition(gridElement));
        }

        private IEnumerator WaitAndShowInNewPosition(GridElement gridElement)
        {
            if (_hideDelay == null)
                _hideDelay = new WaitForSeconds(_currentGridElement.HidingTime);
            yield return _hideDelay;

            gridElement.ScaleToFullSize();
            HidePanel();

            if (_showDelay == null)
                _showDelay = new WaitForSeconds(gridElement.SelectingTime);
            StartCoroutine(WaitAndCheckEquation(_showDelay));
            
            _currentGridElement.ResetColor();
            _currentGridElement = null;
            _coroutine = null;
        }

        public override void HideAndGiveBack()
        {
            base.HideAndGiveBack();

            if (_currentGridElement != null)
                _currentGridElement.UnSelectChangePosition();

            _currentGridElement = null;
        }

    }
}

