using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Start Positions/Start Positions Config")]
public class StartPositionConfig : ScriptableObject
{
    [SerializeField] private LevelStartPosition[] _levelStartPositions;

    public LevelStartPosition GetStartPositions(int level) => _levelStartPositions[level - 1];
}
