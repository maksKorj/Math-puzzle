using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace LevelBuilder
{
    public class GridElement : GridUnit
    {
        [Header("GridElementImage")]
        [SerializeField] protected GridElementImage _contentImage;
        [SerializeField] protected GridElementImage _backgroundImage;
        [SerializeField] private Image _mainBackground;
        
        [Header("Effects")]
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Color _selectColor;
        [SerializeField] private float _animationTime = 1f;

        private GridContent _gridContent;
        private ParticleSystem.MainModule _particleSystemMain;

        #region GridContentProperties
        public GridContent GridContent => _gridContent;
        public bool IsTaken => _gridContent != null;
        public bool IsTakenMathSign => IsTaken && _gridContent is MathSign;
        public MathSign MathSign()
        {
            if (IsTakenMathSign)
                return (MathSign) _gridContent;
            else
            {
                Debug.Break();
                Debug.LogError("Not Math sign");
                return null;
            }
        }
        public bool IsTakenComparisonSigns => IsTaken && _gridContent is ComparisonSign;
        public ComparisonSign ComparisonSign()
        {
            if (IsTakenComparisonSigns)
                return (ComparisonSign) _gridContent;
            else
            {
                Debug.Break();
                Debug.LogError("Not Comparsion Sign!");
                return null;
            }
        }
        public bool IsTakenNumber => IsTaken && _gridContent is Number;
        public int Number()
        {
            if (IsTakenNumber)
                return ((Number)_gridContent).Num;
            else
            {
                Debug.Break();
                Debug.LogError("Not number");
                return -1;
            }
        }
        #endregion

        #region AnimationTimeProperties
        public float SelectingTime => _animationTime * 2;
        public float HidingTime => _animationTime + _particleSystem.main.duration;
        public float ShowingTime => _animationTime;
        public float ChangePozitionEffectTime => _animationTime / 2;
        #endregion

        private void Awake() => _particleSystemMain = _particleSystem.main;

        #region SetContent
        public void SetContent(GridContent gridContent, bool scaleToZero = true)
        {
            if(gridContent == null)
            {
                //Debug.LogError("GridContent is null");
                return;
            }

            if(scaleToZero) ScaleToZero();

            _contentImage.SetImage(gridContent.SymbloSprite, gridContent.SymbolColor);
            _backgroundImage.SetImage(gridContent.BackgroundSprite, gridContent.BackgroundColor);

            _gridContent = gridContent;
        }

        private void ScaleToZero()
        {
            _contentImage.RectTransform.localScale = Vector3.zero;
            _backgroundImage.RectTransform.localScale = Vector3.zero;
        }

        public void SetBackground(Sprite sprite) => _mainBackground.sprite = sprite;
        #endregion

        public void SelectAtEquation()
        {
            var time = _animationTime / 2;
            var sequence = DOTween.Sequence();

            sequence.Append(_backgroundImage.RectTransform.DOScale(0.9f, time).SetEase(Ease.InOutFlash))
                .Insert(0, _contentImage.RectTransform.DOScale(0.9f, time).SetEase(Ease.InOutFlash))
                .Append(_backgroundImage.RectTransform.DOScale(1.05f, time))
                .Insert(time, _contentImage.RectTransform.DOScale(1f, time))
                .Insert(time, _backgroundImage.Image.DOColor(_selectColor, time).SetEase(Ease.InOutElastic))
                .Insert(_animationTime + time, _backgroundImage.Image.DOColor(_backgroundImage.Color, time).SetEase(Ease.InFlash));
        }

        public void ScaleToFullSize()
        {
            _backgroundImage.RectTransform.DOScale(1, _animationTime).SetEase(Ease.OutBounce);
            _contentImage.RectTransform.DOScale(1, _animationTime).SetEase(Ease.OutBounce);
        }

        public void RemoveGridContent() => ResetGridContent();

        #region HideEffect
        public void PlayHideEffect()
        {
            _backgroundImage.RectTransform.DOScale(0.5f, _animationTime).SetEase(Ease.InOutBack);
            _contentImage.RectTransform.DOScale(0.5f, _animationTime).SetEase(Ease.InOutBack).OnComplete(ExplodeAndHide);
        }

        private void ExplodeAndHide()
        {
            _particleSystemMain.startColor = _selectColor;
            _particleSystem.Play();
            ResetGridContent();
        }

        private void ResetGridContent()
        {
            _contentImage.ResetImage();
            _backgroundImage.ResetImage();
            _gridContent = null;
        }
        #endregion

        #region JumpAnimation
        private Sequence _selecting;
        public void JumpAnimation()
        {
            _selecting = DOTween.Sequence();

            _selecting.Append(_backgroundImage.RectTransform.DOPunchScale(-Vector3.one * 0.08f, _animationTime, 1))
                .Insert(0, _contentImage.RectTransform.DOPunchScale(-Vector3.one * 0.08f, _animationTime, 1))
                .SetLoops(-1);
        }

        public void StopJumpAnimation()
        {
            _selecting.Kill();
            ScaleToDefult();
        }

        public void ScaleToDefult()
        {
            _backgroundImage.RectTransform.DOScale(1, 0.4f).SetEase(Ease.OutBounce);
            _contentImage.RectTransform.DOScale(1, 0.4f).SetEase(Ease.OutBounce);
        }
        #endregion

        #region ChangePosition
        public void SelectChangePosition()
        {
            _backgroundImage.Image.DOFade(0.5f, _animationTime / 2);
            _contentImage.Image.DOFade(0.5f, _animationTime / 2);
        }

        public void UnSelectChangePosition()
        {
            _backgroundImage.Image.DOFade(1f, _animationTime / 2);
            _contentImage.Image.DOFade(1f, _animationTime / 2);
        }
        #endregion

        public void ResetColor()
        {
            _backgroundImage.ResetColor();
            _contentImage.ResetColor();
        }
    }
}

