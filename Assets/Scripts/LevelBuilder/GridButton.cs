using UnityEngine;

namespace LevelBuilder
{
    public class GridButton : GridElement
    {
        [Header("Borders")]
        [SerializeField] private GameObject _rightBorder;
        [SerializeField] private GameObject _topBorder; 
        [SerializeField] private GameObject _bottomBorder;

        public UnitGrid UnitGrid { get; set; }

        public void MoveUnit() => UnitGrid.MoveUnit(GridPosition);

        #region BorderAndRotation
        public void ShowRightBorder() => _rightBorder.SetActive(true);
        public void ShowTopBorder() => _topBorder.SetActive(true);
        public void ShowDownBorder() => _bottomBorder.SetActive(true);
        public void RotateContent(Quaternion rotation)
        {
            _contentImage.RectTransform.rotation = Quaternion.Euler(0, 0, rotation.z);
            _backgroundImage.RectTransform.rotation = Quaternion.Euler(0, 0, rotation.z);
        }
        #endregion
    }
}


