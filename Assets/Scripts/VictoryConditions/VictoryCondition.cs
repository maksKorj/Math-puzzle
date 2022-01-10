using System.Collections.Generic;
using UnityEngine;

public abstract class VictoryCondition : ScriptableObject
{
    public abstract void ShowUI();
    public abstract bool IsWin(List<GridContent> gridContents);
}
