using UnityEngine;
using UnityEngine.UI;

namespace LevelBuilder
{
    public class GridElementImage : MonoBehaviour
    {
        private Image _image;
        private RectTransform _rectTransform;
        private Color _color;

        public RectTransform RectTransform => _rectTransform;
        public Image Image => _image;
        public Color Color => _color;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            gameObject.SetActive(false);
        }

        public void SetImage(Sprite sprite, Color color)
        {
            gameObject.SetActive(true);
            _image.sprite = sprite;
            _image.color = color;

            _color = color;
        }

        public void ResetImage()
        {
            gameObject.SetActive(false);
            _rectTransform.localScale = Vector3.one;
        }

        public void ResetColor() => _image.color = _color;
    }
}

