using UnityEngine;
using LevelBuilder;

public class GridAnimation : MonoBehaviour
{
    private GridBuilder _gridBuilder;

    private void Awake()
        => _gridBuilder = GetComponent<GridBuilder>();

    public void ShowGridElements()
    {
        for(int x = 0; x < _gridBuilder.SizeX; x++)
        {
            for(int y = 0; y < _gridBuilder.SizeY; y++)
            {
                if (_gridBuilder.GridElement(x, y) == null)
                    continue;

                _gridBuilder.GridElement(x, y).ShowContent();
            }
        }
    }
}
