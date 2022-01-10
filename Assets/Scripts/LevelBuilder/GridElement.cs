using UnityEngine;

namespace LevelBuilder
{
    public class GridElement : GridUnit
    {
        [SerializeField] private GridElementImage _contentImage;
        [SerializeField] private GridElementImage _backgroundImage;

        private GridContent _gridContent;

        public GridContent GridContent => _gridContent;

        public void SetContent(GridContent gridContent)
        {
            _contentImage.SetImage(gridContent.SymbloSprite, gridContent.SymbolColor);
            _backgroundImage.SetImage(gridContent.BackgroundSprite, gridContent.BackgroundColor);

            _gridContent = gridContent;
        }
    }
}

