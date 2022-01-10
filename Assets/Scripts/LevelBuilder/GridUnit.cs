using UnityEngine;

namespace LevelBuilder
{
    public class GridUnit : MonoBehaviour
    {
        private Vector2Int _gridPosition;
        protected RectTransform _rectTransform;

        public Vector2 WorldPosition => _rectTransform.anchoredPosition;
        public Vector2Int GridPosition => _gridPosition;

        public void SetStartValue(Transform parent, int x, int y)
        {
            _gridPosition = new Vector2Int(x, y);

            _rectTransform = GetComponent<RectTransform>();
            transform.SetParent(parent);
        }

        public void OffsetElement(float size, Vector3 position)
        {
            _rectTransform.sizeDelta = new Vector2(size, size);
            _rectTransform.anchoredPosition = position;
        }

        public void ResetTransform()
        {
            _rectTransform.localScale = Vector2.one;
            _rectTransform.localPosition = new Vector3(_rectTransform.localPosition.x, _rectTransform.localPosition.y, 0);
        }
    }
}

