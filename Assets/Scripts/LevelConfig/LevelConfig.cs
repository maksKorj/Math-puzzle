using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Level Characteristic")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private LevelSettings[] _levelSettings;

    public LevelSettings GetLevelConfig(int level) => _levelSettings[level - 1];
    public int TotalLevelAmount => _levelSettings.Length;
}

[System.Serializable]
public class LevelSettings
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private int _amountOfMoves;
    [SerializeField] private VictoryCondition _victoryCondition;

    public Vector2Int Size => _size;
    public int AmountOfMoves => _amountOfMoves;
    public VictoryCondition VictoryCondition => _victoryCondition;
}
