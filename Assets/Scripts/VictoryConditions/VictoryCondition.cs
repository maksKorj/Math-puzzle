using UnityEngine;

public abstract class VictoryCondition : ScriptableObject
{
    [SerializeField] protected int _moveAmount;

    public int MoveAmount => _moveAmount;

    public abstract void ShowUI();
}
