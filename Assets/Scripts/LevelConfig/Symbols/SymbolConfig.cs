using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Symbol Config/Level Symbols")]
public class SymbolConfig : ScriptableObject
{
    [SerializeField] private GridContent[] _gridContents;

    public GridContent GridContent(int index) => _gridContents[index];
    public int GridContentSize => _gridContents.Length;
}

