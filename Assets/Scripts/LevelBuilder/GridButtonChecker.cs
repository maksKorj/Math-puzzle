using UnityEngine;

namespace LevelBuilder
{
    public class GridButtonChecker
    {
        private Vector2Int _gridSize;

        public GridButtonChecker(Vector2Int gridSize)
        {
            _gridSize = gridSize;
        }

        public bool IsGridButton(int x, int y, out Quaternion rotation)
        {
            if (x == 0)
            {
                rotation = Quaternion.Euler(0f, 0f, 180f);
                return true;
            }

            if (x == _gridSize.x - 1)
            {
                rotation = Quaternion.identity;
                return true;
            }

            if (y == 0)
            {
                rotation = Quaternion.Euler(0f, 0f, 270f);
                return true;
            }

            if (y == _gridSize.y - 1)
            {
                rotation = Quaternion.Euler(0f, 0f, 90f);
                return true;
            }

            rotation = Quaternion.identity;
            return false;
        }

        public void CheckGridButtonBorder(int x, int y, GridButton gridButton)
        {
            int maxIndexX = _gridSize.x - 1;
            int maxIndexY = _gridSize.y - 1;

            gridButton.ShowRightBorder();

            if (x == 0)
            {
                if (y == 1)
                    gridButton.ShowTopBorder();
                else if (y == maxIndexY - 1)
                    gridButton.ShowDownBorder();
            }

            if (x == maxIndexX)
            {
                if (y == 1)
                    gridButton.ShowDownBorder();
                else if (y == maxIndexY - 1)
                    gridButton.ShowTopBorder();
            }

            if (y == 0)
            {
                if (x == 1)
                    gridButton.ShowDownBorder();
                else if (x == maxIndexX - 1)
                    gridButton.ShowTopBorder();
            }

            if (y == maxIndexY)
            {
                if (x == 1)
                    gridButton.ShowTopBorder();
                else if (x == maxIndexX - 1)
                    gridButton.ShowDownBorder();
            }
        }
    }
}
