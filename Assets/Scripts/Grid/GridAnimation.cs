using UnityEngine;
using LevelBuilder;
using System;

public class GridAnimation : MonoBehaviour
{
    private GridBuilder _gridBuilder;

    private void Awake()
        => _gridBuilder = GetComponent<GridBuilder>();

    public void ShowGridElements()
        => MakeAction((int x, int y) => _gridBuilder.GridElement(x, y).ScaleToFullSize());

    public void SelectAll() 
        => MakeAction((int x, int y) => _gridBuilder.GridElement(x, y).JumpAnimation());

    public void UnSelectAll()
        => MakeAction((int x, int y) => _gridBuilder.GridElement(x, y).StopJumpAnimation());

    private void MakeAction(Action<int, int> action)
    {
        for (int x = 0; x < _gridBuilder.SizeX; x++)
        {
            for (int y = 0; y < _gridBuilder.SizeY; y++)
            {
                if (_gridBuilder.GridElement(x, y) == null)
                    continue;

                action(x, y);
            }
        }
    }
}
