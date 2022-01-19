using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Boosters
{
    public class SelectSymbolScreen : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SelectSymbolElement[] _numbers, _mathSigns, _comparsionSigns;
        [SerializeField] private Symbols _symbols;

        private PointerEventData _pointerEventData;
        private Touch _touch;
        private List<RaycastResult> _results = new List<RaycastResult>();

        private Action<GridContent> GiveGridContent;
        private Action CloseWindow;

        private void Awake()
        {
            for (int i = 0; i < _numbers.Length; i++)
                _numbers[i].SetContent(_symbols.Numbers[i]);

            for (int i = 0; i < _mathSigns.Length; i++)
                _mathSigns[i].SetContent(_symbols.MathSigns[i]);

            for (int i = 0; i < _comparsionSigns.Length; i++)
                _comparsionSigns[i].SetContent(_symbols.ComparisonSigns[i]);


            _pointerEventData = new PointerEventData(_eventSystem);
            gameObject.SetActive(false);
        }

        private void Update() => CheckTap();

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

            _pointerEventData.position = _touch.position;
            _raycaster.Raycast(_pointerEventData, _results);

            foreach (RaycastResult result in _results)
            {
                if (IsCorrectRaycastResult(result))
                    return;
            }
        }

        private bool IsCorrectRaycastResult(RaycastResult result)
        {
            if(result.gameObject.TryGetComponent(out SelectSymbolElement selectSymbolElement))
            {
                SetGridContent(selectSymbolElement.GridContent);
                return true;
            }

            return false;
        }

        private void SetGridContent(GridContent gridContent)
        {
            GiveGridContent(gridContent);
            gameObject.SetActive(false);
        }

        public void Hide()
        {
            CloseWindow();
            gameObject.SetActive(false);
        }

        public void ShowWindow(Action<GridContent> setContent, Action hide)
        {
            gameObject.SetActive(true);
            GiveGridContent = setContent;
            CloseWindow = hide;
        }
    }
}


