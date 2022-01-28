using UnityEngine;

namespace LevelBuilder
{
    public class GridButton : GridElement
    {
        [Header("Borders")]
        [SerializeField] private GameObject _rightBorder;
        [SerializeField] private GameObject _topBorder; 
        [SerializeField] private GameObject _bottomBorder;

        [SerializeField] private RectTransform _contentTransform;

        public UnitGrid UnitGrid { get; set; }

        public void MoveUnit() => UnitGrid.MoveUnit(GridPosition);

        #region BorderAndRotation
        public void ShowRightBorder() => _rightBorder.SetActive(true);
        public void ShowTopBorder() => _topBorder.SetActive(true);
        public void ShowDownBorder() => _bottomBorder.SetActive(true);
        public void RotateContent(Quaternion rotation)
        {
            if (rotation.z == 0)
                return;

            _contentImage.RectTransform.rotation = Quaternion.Euler(0, 0, Mathf.RoundToInt(rotation.z) - 1);
            _backgroundImage.RectTransform.rotation = Quaternion.Euler(0, 0, Mathf.RoundToInt(rotation.z) - 1);
        }
        #endregion

        public void OffsetContentToLeft() => SetOffset(25, 15, 20, 20);
        public void OffsetContentToBottom() => SetOffset(20, 20, 15, 25);
        public void OffsetContentToTop() => SetOffset(20, 20, 25, 15);

        private void SetOffset(int top, int bottom, int left, int right)
        {
            _contentTransform.Top(top);
            _contentTransform.Bottom(bottom);
            _contentTransform.Left(left);
            _contentTransform.Right(right);
        }
    }
}


