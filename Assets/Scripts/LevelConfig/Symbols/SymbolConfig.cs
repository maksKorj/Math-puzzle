using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Symbol Config/Level Symbols")]
public class SymbolConfig : ScriptableObject
{
    [SerializeField] private GridContent[] _gridContents;
    [SerializeField] private bool _isRandomOutput;

    public GridContent GridContent(int index) => _gridContents[index];
    public int GridContentAmount => _gridContents.Length;
    public bool IsRandomOutput => _isRandomOutput;
}

