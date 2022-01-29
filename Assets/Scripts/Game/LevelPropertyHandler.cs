using UnityEngine;

public class LevelPropertyHandler : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private RandomVictoryCondition[] _victoryConditions;

    private LevelSettings _levelSettings;
    private RandomVictoryCondition _victoryCondition;
    private Vector2Int _size;

    private void Awake()
        => _levelSettings = _levelConfig.GetLevelConfig(PlayerSaver.LoadPlayerLevel());

    public bool IsContainedLevel() => _levelSettings != null;
    public bool HasPositionConfig() => IsContainedLevel() && _levelSettings.StartPositionConfig != null;
    public bool HasSymbolConfig() => IsContainedLevel() && _levelSettings.SymbolConfig != null;

    public Vector2Int Size()
    {
        if (IsContainedLevel())
            return _levelSettings.Size;
        else
        {
            if(_size == Vector2Int.zero)
            {
                int x = Random.Range(8, 11);
                int y = Random.Range(x, x + 2);

                _size = new Vector2Int(x, y);
            }
        }
        
        return _size;
    }

    public int AmountOfMoves()
    {
        if (IsContainedLevel())
            return _levelSettings.VictoryCondition.MoveAmount;
        else
        {
            CheckVictoryCondition();
            return _victoryCondition.MoveAmount;
        }
    }

    public VictoryCondition VictoryCondition()
    {
        if (IsContainedLevel())
            return _levelSettings.VictoryCondition;
        else
        {
            CheckVictoryCondition();
            return _victoryCondition;
        }
    }

    private void CheckVictoryCondition()
    {
        if (_victoryCondition == null)
        {
            _victoryCondition = _victoryConditions[Random.Range(0, _victoryConditions.Length)];
            _victoryCondition.SetValue(PlayerSaver.LoadPlayerLevel());
        }
    }

    public SymbolConfig SymbolConfig()
    {
        if (HasSymbolConfig())
            return _levelSettings.SymbolConfig;
        else
        {
            ShowError();
            return null;
        }
    }

    public LevelStartPosition StartPositionConfig()
    {
        if (HasSymbolConfig())
            return _levelSettings.StartPositionConfig;
        else
        {
            ShowError();
            return null;
        }
    }

    private static void ShowError()
    {
        Debug.LogError("LevelSettings is null");
        Debug.Break();
    }
}
