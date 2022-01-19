using LevelBuilder;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boosters
{
    public class SwipeHandler : MainScreen
    {
        private GridElement _currentGridElement;
        private Coroutine _coroutine;
        private WaitForSeconds _hideDelay, _showDelay, _selectDelay;

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
                if (result.gameObject.TryGetComponent(out GridElement gridElement) && gridElement.IsTaken 
                    && _currentGridElement != gridElement)
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
            gridElement.StopJumpAnimation();

            StartCoroutine(WaitAndDisplay(gridElement));
        }

        private IEnumerator WaitAndDisplay(GridElement gridElement)
        {
            if (_selectDelay == null)
                _selectDelay = new WaitForSeconds(gridElement.ShowingTime / 2);

            yield return _selectDelay;

            _currentGridElement = gridElement;
            _currentGridElement.SelectChangePosition();
            _text.text = "Choose chip to switch";
            _canClick = true;
        }

        private void MoveTo(GridElement gridElement)
        {
            _canClick = false;

            if (_coroutine == null)
                _coroutine = StartCoroutine(WaitAndShowInNewPosition(gridElement));
        }

        private IEnumerator WaitAndShowInNewPosition(GridElement gridElement)
        {
            SaveContentAndHide(gridElement, out GridContent firstContent, out GridContent secondContent);

            if (_hideDelay == null)
                _hideDelay = new WaitForSeconds(_currentGridElement.HidingTime);
            yield return _hideDelay;

            ShowContents(gridElement, firstContent, secondContent);

            if (_showDelay == null)
                _showDelay = new WaitForSeconds(_currentGridElement.ShowingTime);

            StartCoroutine(WaitAndCheckEquation(_showDelay));

            _currentGridElement = null;
            _coroutine = null;
        }
        
        private void SaveContentAndHide(GridElement gridElement, out GridContent content, out GridContent cont)
        {
            content = gridElement.GridContent;
            cont = _currentGridElement.GridContent;
            _mainScreenProperty.GridAnimation.UnSelectAll();
            _currentGridElement.PlayHideEffect();
            gridElement.PlayHideEffect();
        }

        private void ShowContents(GridElement gridElement, GridContent firstContent, GridContent secondContent)
        {
            _currentGridElement.ResetColor();
            gridElement.SetContent(secondContent);
            _currentGridElement.SetContent(firstContent);

            gridElement.ScaleToFullSize();
            _currentGridElement.ScaleToFullSize();
            HidePanel();
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

