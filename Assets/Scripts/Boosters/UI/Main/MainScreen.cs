using System.Collections.Generic;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;
using System;

namespace Boosters
{
    public abstract class MainScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] protected TextMeshProUGUI _text;
        [SerializeField] protected MainScreenProperty _mainScreenProperty;

        private Type _boosterType;
        private Touch _touch;
        private List<RaycastResult> _results = new List<RaycastResult>();

        protected bool _canClick = true;

        private void Update()
        {
            if(_panel.activeInHierarchy && _canClick)
                CheckTap();
        }
        
        #region Tap
        private void CheckTap()
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);
                if (_touch.phase == TouchPhase.Began)
                {
                    CheckRaycast();
                }
            }
        }

        private void CheckRaycast()
        {
            _results.Clear();

            _mainScreenProperty.PointerEventData.position = _touch.position;
            _mainScreenProperty.Raycaster.Raycast(_mainScreenProperty.PointerEventData, _results);

            foreach (RaycastResult result in _results)
            {
                if (IsCorrectRaycastResult(result))
                    return;
            }
        }

        protected abstract bool IsCorrectRaycastResult(RaycastResult result);
        #endregion

        public void Initialize(Type boosterType) => _boosterType = boosterType;

        public virtual void Apply()
        {
            _mainScreenProperty.UnitGrid.BlockShoting();
            _panel.SetActive(true);
            _mainScreenProperty.GridAnimation.SelectAll();
        }

        public virtual void HideAndGiveBack()
        {
            Hide();
            _mainScreenProperty.GridAnimation.UnSelectAll();
            _mainScreenProperty.BoosterManager.ReceiveBack(_boosterType);
            _mainScreenProperty.UnitGrid.UnBlockShoting();
        }

        protected void Hide()
        {
            _panel.SetActive(false);
            _canClick = true;
        }

        protected void HidePanel() => _panel.SetActive(false);

        protected IEnumerator WaitAndCheckEquation(WaitForSeconds delay)
        {
            yield return delay;
            _mainScreenProperty.EquationChecker.Check();
            Hide();
        }
    }
}

