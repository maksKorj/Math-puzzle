using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Level Characteristic")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private LevelSettings[] _levelSettings;

    public LevelSettings GetLevelConfig(int level)
    {
        for(int i = 0; i < _levelSettings.Length; i++)
        {
            if (_levelSettings[i].Level == level)
                return _levelSettings[i];
        }

        return null;
    }
}

[System.Serializable]
public class LevelSettings
{
    [SerializeField] private int _level;

    [SerializeField] private Vector2Int _size;
    [SerializeField] private SymbolConfig _symbolConfig;
    [SerializeField] private LevelStartPosition _startPositionConfig;
    [SerializeField] private VictoryCondition _victoryCondition;

    public Vector2Int Size => _size;
    public VictoryCondition VictoryCondition => _victoryCondition;
    public int Level => _level;
    public SymbolConfig SymbolConfig => _symbolConfig;
    public LevelStartPosition StartPositionConfig => _startPositionConfig;
}
