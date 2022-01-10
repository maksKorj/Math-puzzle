using UnityEngine;

namespace LevelBuilder
{
    public class MoveUnit : MonoBehaviour
    {
        [SerializeField] private GridElementImage _contentImage;
        [SerializeField] private GridElementImage _backgroundImage;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetStartSize(float size)
        {
            _rectTransform.sizeDelta = new Vector2(size, size);
            transform.SetAsLastSibling();
        }

        public void MoveTo(GridContent gridContent)
        {
            _contentImage.SetImage(gridContent.SymbloSprite, gridContent.SymbolColor);
            _backgroundImage.SetImage(gridContent.BackgroundSprite, gridContent.BackgroundColor);
            //Move...
            //LeanTween or DoTween
        }
    }
}

