using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Boosters
{
    public class MainScreenProperty : MonoBehaviour
    {
        [SerializeField] private BoosterManager _boosterManager;
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private GridAnimation _gridAnimation;
        [SerializeField] private EquationChecker _equationChecker;
        [SerializeField] private UnitGrid _unitGrid;

        public BoosterManager BoosterManager => _boosterManager;
        public GraphicRaycaster Raycaster => _raycaster;
        public GridAnimation GridAnimation => _gridAnimation;
        public EquationChecker EquationChecker => _equationChecker;
        public UnitGrid UnitGrid => _unitGrid;

        public PointerEventData PointerEventData { get; private set; }

        private void Awake()
            => PointerEventData = new PointerEventData(_eventSystem);
    }

}
