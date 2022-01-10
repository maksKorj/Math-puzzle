using UnityEngine;
using UnityEngine.UI;

namespace LevelBuilder
{
    public class GridElementImage : MonoBehaviour
    {
        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        public void SetImage(Sprite sprite, Color color)
        {
            gameObject.SetActive(true);
            _image.sprite = sprite;
            _image.color = color;
        }

        public void ResetImage() => gameObject.SetActive(false);
    }
}

