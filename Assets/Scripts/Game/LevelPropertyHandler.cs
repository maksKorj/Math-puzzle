using UnityEngine;

public class LevelPropertyHandler : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private VictoryCondition[] _victoryConditions;

    private LevelSettings _levelSettings;
    private VictoryCondition _victoryCondition;
    private Vector2Int _size;

    private void Awake()
        => _levelSettings = _levelConfig.GetLevelConfig(PlayerSaver.LoadPlayerLevel());

    public bool IsContainedLevel() => _levelSettings != null;

    public Vector2Int Size()
    {
        if (IsContainedLevel())
            return _levelSettings.Size;
        else
        {
            if(_size == Vector2Int.zero)
            {
                int x = Random.Range(7, 10);
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
            if (_victoryCondition == null)
                _victoryCondition = _victoryConditions[Random.Range(0, _victoryConditions.Length)];

            return _victoryCondition.MoveAmount;
        }
    }
    
    public VictoryCondition VictoryCondition()
    {
        if (IsContainedLevel())
            return _levelSettings.VictoryCondition;
        else
        {
            if (_victoryCondition == null)
                _victoryCondition = _victoryConditions[Random.Range(0, _victoryConditions.Length)];


            return _victoryCondition;
        }
    }
    
    public SymbolConfig SymbolConfig()
    {
        if (IsContainedLevel())
            return _levelSettings.SymbolConfig;
        else
        {
            ShowError();
            return null;
        }
    }

    public LevelStartPosition StartPositionConfig()
    {
        if (IsContainedLevel())
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
