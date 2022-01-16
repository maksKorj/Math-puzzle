using UnityEngine;

namespace LevelBuilder
{
    public class GridBuilder : MonoBehaviour
    {
        [SerializeField] private RectTransform _canvas;
        [Header("Config")]
        [SerializeField] private LevelPropertyHandler _levelPropertyHandler;

        [Header("Grid Elements")]
        [SerializeField] private GridElement _gridElement;
        [SerializeField] private GridButton _gridButton;
        [SerializeField] private UnitMover _unitMover;

        private RectTransform _rectTransform;
        private GridElement[,] _gridElements;
        private float _unitDiameter;
        private Vector2Int _gridSize;
        private UnitGrid _unitGrid;

        public GridElement GridElement(int x, int y) => _gridElements[x, y];
        public GridElement GridElement(Vector2Int gridPosition) => _gridElements[gridPosition.x, gridPosition.y];
        public Vector2 GridElementWorldPosition(Vector2Int gridPosition) 
            => _gridElements[gridPosition.x, gridPosition.y].WorldPosition;
        public int SizeX => _gridElements.GetLength(0);
        public int SizeY => _gridElements.GetLength(1);

        private void Awake() => CreateGrid();

        private void Start()
        {
            SetUnitDiameter();
            OffsetGridElement();
            SetStartGridValue();
            ResetGridElementTranform();

            //ToDO
            FindObjectOfType<SymbolGiver>().SetCharacter();
        }

        #region CreatingGridElements
        private void CreateGrid()
        {
            _canvas.gameObject.GetComponent<TabletScaler>().CheckResolution();

            _gridSize = _levelPropertyHandler.Size();
            _rectTransform = GetComponent<RectTransform>();
            _unitGrid = GetComponent<UnitGrid>();
            _gridElements = new GridElement[_gridSize.x, _gridSize.y];

            var gridButtonChecker = new GridButtonChecker(_gridSize);

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    if (InvalidIndex(x, y))
                        continue;

                    if (gridButtonChecker.IsGridButton(x, y, out Quaternion rotation))
                    {
                        CreateButton(gridButtonChecker, x, y, rotation);
                        continue;
                    }

                    CreateGridElement(x, y);
                }
            }
        }

        private void CreateButton(GridButtonChecker gridButtonChecker, int x, int y, Quaternion rotation)
        {
            var gridButton = Instantiate(_gridButton, Vector3.zero, rotation);
            gridButton.SetStartValue(transform, x, y);
            gridButton.RotateContent(rotation);
            gridButton.UnitGrid = _unitGrid;
            gridButtonChecker.CheckGridButtonBorder(x, y, gridButton);

            _gridElements[x, y] = gridButton;
        }

        private void CreateGridElement(int x, int y)
        {
            var gridElement = Instantiate(_gridElement, Vector3.zero, Quaternion.identity);
            gridElement.SetStartValue(transform, x, y);

            _gridElements[x, y] = gridElement;
        }
        #endregion

        #region GridElementSetUp
        private void SetUnitDiameter()
        {
            var gridWorldSize = _rectTransform.rect.size * 0.95f;

            if(_canvas.gameObject.GetComponent<TabletScaler>().IsTablet)
            {
                _unitDiameter = Mathf.RoundToInt(gridWorldSize.y / _gridSize.y);
            }
            else
            {
                if (_gridSize.x >= _gridSize.y)
                    _unitDiameter = Mathf.RoundToInt(gridWorldSize.x / _gridSize.x);
                else
                    _unitDiameter = Mathf.RoundToInt(gridWorldSize.y / _gridSize.y);
            }
        }

        private void OffsetGridElement()
        {
            var worldBottomLeft = GetWorldBottomLeftPosition();

            for (int x = 0; x < _gridSize.x; x++)
            {
                for (int y = 0; y < _gridSize.y; y++)
                {
                    if (InvalidIndex(x, y))
                        continue;

                    _gridElements[x, y].OffsetElement(_unitDiameter, GetUnitWorldPosition(worldBottomLeft, x, y));
                }
            }

            _unitMover.SetStartSize(_unitDiameter);
        }

        private void SetStartGridValue()
        {
            if(_levelPropertyHandler.IsContainedLevel())
            {
                foreach (var gridContent in _levelPropertyHandler.StartPositionConfig().TakenGridElements)
                {
                    var gridElement = _gridElements[gridContent.Position.x, gridContent.Position.y];
                    gridElement.SetContent(gridContent.Content);
                }
            }
            else
                GetComponent<GridAddition>().AddElementsToGrid(true);

            //ToDo
            GetComponent<GridAnimation>().ShowGridElements();
        }

        private void ResetGridElementTranform()
        {
            for (int x = 0; x < _gridElements.GetLength(0); x++)
            {
                for (int y = 0; y < _gridElements.GetLength(1); y++)
                {
                    if (_gridElements[x, y] != null)
                        _gridElements[x, y].ResetTransform();
                }
            }
        }
        #endregion

        #region HelperFunction
        private bool InvalidIndex(int x, int y) =>
            x == 0 && y == 0
            || x == 0 && y == _gridSize.y - 1
            || x == _gridSize.x - 1 && y == 0
            || x == _gridSize.x - 1 && y == _gridSize.y - 1;

        private Vector3 GetWorldBottomLeftPosition() => new Vector3((_canvas.rect.width - _unitDiameter * _gridSize.x) / 2,
           (_rectTransform.rect.size.y - _unitDiameter * _gridSize.y) / 2, 0);

        private Vector3 GetUnitWorldPosition(Vector3 worldBottomLeft, int x, int y)
        {
            var unitRadius = _unitDiameter / 2;
            var horizontalMiddle = worldBottomLeft + Vector3.right * (x * _unitDiameter + unitRadius);
            return horizontalMiddle + Vector3.up * (y * _unitDiameter + unitRadius);
        }

        #endregion
    }
}


