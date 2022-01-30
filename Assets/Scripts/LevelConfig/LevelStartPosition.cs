using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Start Positions/Level Start Positions")]
public class LevelStartPosition : ScriptableObject
{
    //ToDo
    [SerializeField] private List<GridElementContent> _gridElementContents;

    public IEnumerable<GridElementContent> TakenGridElements => _gridElementContents;
    public int GridContentAmount => _gridElementContents.Count;
}

[System.Serializable]
public class GridElementContent
{
    [SerializeField] private Vector2Int _position;
    [SerializeField] private GridContent _gridContent;

    public Vector2Int Position => _position;
    public GridContent Content => _gridContent;
}