using UnityEngine;
using LevelBuilder;

public class UnitGrid : MonoBehaviour
{
    [SerializeField] private UnitMover _unitMover;
    [SerializeField] private SymbolGiver _symbolGiver;
    [SerializeField] private EquationChecker _equationChecker;
    [SerializeField] private MoveCounter _moveCounter;

    private GridBuilder _gridBuilder;
    private UnitGridChecker _unitGridChecker;
    private Vector2Int _endPosition = new Vector2Int();
    private GridContent _currentGridContent;
    private bool _movingThrougGrid;

    public bool CanShoot { get; set; } = true;

    private void Awake()
    {
        _gridBuilder = GetComponent<GridBuilder>();

        _unitGridChecker = GetComponent<UnitGridChecker>();
        _unitGridChecker.GridBuilder = _gridBuilder;

        _unitMover.OnEndMoving += EndMoving;
        _symbolGiver.SymbolGiverVisual.OnEndGivingSymbol += EndGivingSymbol;
    }

    private void OnDisable()
    {
        _unitMover.OnEndMoving -= EndMoving;
        _symbolGiver.SymbolGiverVisual.OnEndGivingSymbol -= EndGivingSymbol;
    }

    public void MoveUnit(Vector2Int startGridPosition)
    {
        if (CanShoot == false)
            return;

        CanShoot = false;

        if (startGridPosition.x == 0)
        {
            CheckMoveRight(startGridPosition);
            return;
        }

        if (startGridPosition.x == _gridBuilder.SizeX - 1)
        {
            CheckMoveLeft(startGridPosition);
            return;
        }

        if (startGridPosition.y == 0)
        {
            CheckMoveUp(startGridPosition);
            return;
        }

        if (startGridPosition.y == _gridBuilder.SizeY - 1)
        {
            CheckMoveDown(startGridPosition);
            return;
        }

        CanShoot = true;
    }

    #region Horizontal
    private void CheckMoveRight(Vector2Int startGridPosition)
    {
        if (_unitGridChecker.CanMoveRightToTakenUnit(startGridPosition.y, out int endX))
        {
            MoveHorizontal(startGridPosition, endX);
        }
        else
        {
            if (endX >= 0)
                MoveHorizontal(startGridPosition, endX, true);
        }
    }

    private void CheckMoveLeft(Vector2Int startGridPosition)
    {
        if (_unitGridChecker.CanMoveLeftToTakenUnit(startGridPosition.y, out int endX))
        {
            MoveHorizontal(startGridPosition, endX);
        }
        else
        {
            if (endX >= 0)
                MoveHorizontal(startGridPosition, endX, true);
        }
    }

    private void MoveHorizontal(Vector2Int startGridPosition, int endX, bool blowUpInEnd = false)
    {
        _moveCounter.RemoveMove();
        _currentGridContent = _symbolGiver.GetNextSymbol();

        _endPosition.x = endX;
        _endPosition.y = startGridPosition.y;
        _unitMover.MoveTo(_currentGridContent, _gridBuilder.GridElementWorldPosition(startGridPosition),
            _gridBuilder.GridElementWorldPosition(_endPosition), blowUpInEnd);

        _movingThrougGrid = blowUpInEnd;
    }
    #endregion

    #region Vertical
    private void CheckMoveUp(Vector2Int startGridPosition)
    {
        if (_unitGridChecker.CanMoveUpToTakenUnit(startGridPosition.x, out int endY))
        {
            MoveVertical(startGridPosition, endY);
        }
        else
        {
            if (endY >= 0)
                MoveVertical(startGridPosition, endY, true);
        }
    }


    private void CheckMoveDown(Vector2Int startGridPosition)
    {
        if (_unitGridChecker.CanMoveDownToTakenUnit(startGridPosition.x, out int endY))
        {
            MoveVertical(startGridPosition, endY);
        }
        else
        {
            if (endY >= 0)
                MoveVertical(startGridPosition, endY, true);
        }
    }

    private void MoveVertical(Vector2Int startGridPosition, int endY, bool blowUpInEnd = false)
    {
        _moveCounter.RemoveMove();
        _currentGridContent = _symbolGiver.GetNextSymbol();

        _endPosition.x = startGridPosition.x;
        _endPosition.y = endY;
        _unitMover.MoveTo(_currentGridContent, _gridBuilder.GridElementWorldPosition(startGridPosition),
            _gridBuilder.GridElementWorldPosition(_endPosition), blowUpInEnd);

        _movingThrougGrid = blowUpInEnd;
    }
    #endregion

    private void EndMoving()
    {
        if (_movingThrougGrid)
        {
            CanShoot = true;
            _symbolGiver.SetCharacter();
            return;
        }

        _gridBuilder.GridElement(_endPosition).SetContent(_currentGridContent, false);
        _equationChecker.Check(_endPosition);
    }

    private void EndGivingSymbol() => CanShoot = true;
}
