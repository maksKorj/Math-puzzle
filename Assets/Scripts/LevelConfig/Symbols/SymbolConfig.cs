using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Symbol Config/Symbol Config")]
public class SymbolConfig : ScriptableObject
{
    [SerializeField] private LevelSymbol[] _levelSymbols;

    public LevelSymbol GetLevelSymbols(int level) => _levelSymbols[level - 1];
}

[System.Serializable]
public class LevelSymbol
{
    [SerializeField] private bool _isRandomOutput;
    [SerializeField] private GridContent[] _gridContents;

    public bool IsRandomOutput => _isRandomOutput;
    public GridContent GridContent(int index) => _gridContents[index];
    public int GridContentSize => _gridContents.Length;
}
