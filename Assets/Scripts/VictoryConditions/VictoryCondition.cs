using System.Collections.Generic;
using UnityEngine;

public abstract class VictoryCondition : ScriptableObject
{
    [SerializeField] private int _moveAmount;

    public int MoveAmount => _moveAmount;

    public abstract void ShowUI();
    public abstract bool IsWin(List<GridContent> gridContents);
}
