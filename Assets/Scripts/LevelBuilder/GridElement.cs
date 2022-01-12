using UnityEngine;
using DG.Tweening;

namespace LevelBuilder
{
    public class GridElement : GridUnit
    {
        [Header("GridElementImage")]
        [SerializeField] protected GridElementImage _contentImage;
        [SerializeField] protected GridElementImage _backgroundImage;
        
        [Header("Effects")]
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Color _selectColor;
        [SerializeField] private float _selectedTime = 1f;

        private GridContent _gridContent;
        private ParticleSystem.MainModule _particleSystemMain;

        public GridContent GridContent => _gridContent;
        public bool IsTaken => _gridContent != null;
        public bool IsTakenMathSign => IsTaken && _gridContent is MathSign;
        public bool IsTakenÑomparisonSigns => IsTaken && _gridContent is ComparisonSign;
        public bool IsTakenNumber => IsTaken && _gridContent is Number;
        public float SelectingTime => _selectedTime * 2;
        public float HidingTime => _selectedTime + _particleSystem.main.duration;
        public float ShowingTime => _selectedTime;

        private void Awake()
        {
            _particleSystemMain = _particleSystem.main;
        }

        public void SetContent(GridContent gridContent)
        {
            //ScaleToZero();

            _contentImage.SetImage(gridContent.SymbloSprite, gridContent.SymbolColor);
            _backgroundImage.SetImage(gridContent.BackgroundSprite, gridContent.BackgroundColor);

            _gridContent = gridContent;
        }

        public void SelectEquation()
        {
            var time = _selectedTime / 2;
            var sequence = DOTween.Sequence();

            sequence.Append(_backgroundImage.RectTransform.DOScale(0.9f, time).SetEase(Ease.InOutFlash))
                .Insert(0, _contentImage.RectTransform.DOScale(0.9f, time).SetEase(Ease.InOutFlash))
                .Append(_backgroundImage.RectTransform.DOScale(1.05f, time))
                .Insert(time, _contentImage.RectTransform.DOScale(1f, time))
                .Insert(time, _backgroundImage.Image.DOColor(_selectColor, time).SetEase(Ease.InOutElastic))
                .Insert(_selectedTime + time, _backgroundImage.Image.DOColor(_backgroundImage.Color, time).SetEase(Ease.InFlash));
        }

        public void ShowContent(GridContent gridContent)
        {
            ScaleToZero();
            SetContent(gridContent);

            _backgroundImage.RectTransform.DOScale(1, _selectedTime).SetEase(Ease.OutBounce);
            _contentImage.RectTransform.DOScale(1, _selectedTime).SetEase(Ease.OutBounce);
        }

        private void ScaleToZero()
        {
            _contentImage.RectTransform.localScale = Vector3.zero;
            _backgroundImage.RectTransform.localScale = Vector3.zero;
        }

        #region HideWithEffect
        public void HideAfterSelect()
        {
            //_backgroundImage.Image.DOColor(_selectColor, _selectedTime / 2);
            _backgroundImage.RectTransform.DOScale(0.5f, _selectedTime).SetEase(Ease.InOutBack);
            _contentImage.RectTransform.DOScale(0.5f, _selectedTime).SetEase(Ease.InOutBack).OnComplete(BoomAndHide);
        }

        private void BoomAndHide()
        {
            _particleSystemMain.startColor = _selectColor;
            _particleSystem.Play();
            Hide();
        }

        private void Hide()
        {
            _contentImage.ResetImage();
            _backgroundImage.ResetImage();
            _gridContent = null;
        }
        #endregion
    }
}

