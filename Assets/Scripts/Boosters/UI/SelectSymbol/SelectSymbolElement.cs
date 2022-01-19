using UnityEngine;
using UnityEngine.UI;

namespace Boosters
{
    public class SelectSymbolElement : MonoBehaviour
    {
        [SerializeField] private Image _background, _content;
        
        private GridContent _gridContent;
        public GridContent GridContent => _gridContent;

        public void SetContent(GridContent gridContent)
        {
            _background.sprite = gridContent.BackgroundSprite;
            _background.color = gridContent.BackgroundColor;
            _content.sprite = gridContent.SymbloSprite;
            _content.color = gridContent.SymbolColor;

            _gridContent = gridContent;
        }
    }
}

