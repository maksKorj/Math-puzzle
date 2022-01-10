using UnityEngine;

namespace LevelBuilder
{
    public class GridButton : GridElement
    {
        [Header("Borders")]
        [SerializeField] private GameObject _rightBorder;
        [SerializeField] private GameObject _topBorder; 
        [SerializeField] private GameObject _bottomBorder;

        public void MoveChatacter()
        {
            //SetPosition
        }

        #region ShowBorders
        public void ShowRightBorder() => _rightBorder.SetActive(true);
        public void ShowTopBorder() => _topBorder.SetActive(true);
        public void ShowDownBorder() => _bottomBorder.SetActive(true);
        #endregion
    }
}


