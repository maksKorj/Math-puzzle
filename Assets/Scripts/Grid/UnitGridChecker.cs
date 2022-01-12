using UnityEngine;
using LevelBuilder;

public class UnitGridChecker : MonoBehaviour
{
    public GridBuilder GridBuilder { get; set; }

    public bool CanMoveRightToTakenUnit(int y, out int endX)
    {
        endX = -1;

        for (int x = 0; x < GridBuilder.SizeX; x++)
        {
            if(GridBuilder.GridElement(x, y).IsTaken)
            {
                if (x - 1 >= 0)
                {
                    endX = x - 1;
                    return true;
                }
                else
                    return false;
            }
        }

        endX = GridBuilder.SizeX - 1;
        return false;
    }

    public bool CanMoveLeftToTakenUnit(int y, out int endX)
    {
        endX = -1;

        for (int x = GridBuilder.SizeX - 1; x >= 0; x--)
        {
            if (GridBuilder.GridElement(x, y).IsTaken)
            {
                if (x + 1 < GridBuilder.SizeX)
                {
                    endX = x + 1;
                    return true;
                }
                else
                    return false;
            }
        }

        endX = 0;
        return false;
    }

    public bool CanMoveUpToTakenUnit(int x, out int endY)
    {
        endY = -1;

        for (int y = 0; y < GridBuilder.SizeY; y++)
        {
            if (GridBuilder.GridElement(x, y).IsTaken)
            {
                if (y - 1 >= 0)
                {
                    endY = y - 1;
                    return true;
                }
                else
                    return false;
            }
        }

        endY = GridBuilder.SizeY - 1;
        return false;
    }

    public bool CanMoveDownToTakenUnit(int x, out int endY)
    {
        endY = -1;

        for (int y = GridBuilder.SizeY - 1; y >= 0; y--)
        {
            if (GridBuilder.GridElement(x, y).IsTaken)
            {
                if (y + 1 < GridBuilder.SizeY)
                {
                    endY = y + 1;
                    return true;
                }
                else
                    return false;
            }
        }

        endY = 0;
        return false;
    }
}
