using UnityEngine;
using DG.Tweening;
using System;
using System.Collections;

namespace LevelBuilder
{
    public class UnitMover : MonoBehaviour
    {
        [SerializeField] private GridElementImage _contentImage;
        [SerializeField] private GridElementImage _backgroundImage;
        [SerializeField] private ParticleSystem _boom;
        [SerializeField] private float _speed;
        [SerializeField] private SymbolGiverParticle _symbolGiverParticle;

        private bool _canMove = false;
        private RectTransform _rectTransform;
        private ParticleSystem.MainModule _boomMainModule;
        private WaitUntil _moveDelay, _scaleDelay;

        public event Action OnEndMoving;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _boomMainModule = _boom.main;

            _moveDelay = new WaitUntil(() => _symbolGiverParticle.IsMovingToPosition != true);
            _scaleDelay = new WaitUntil(() => _canMove == true);
        }

        public void SetStartSize(float size)
        {
            _rectTransform.sizeDelta = new Vector2(size, size);
            transform.SetAsLastSibling();
        }

        public void MoveTo(GridContent gridContent, Vector2 startPosition, Vector2 endPosition, bool blowUpInEnd = false)
        {
            _symbolGiverParticle.MoveTo(startPosition, gridContent.BackgroundColor);

            _backgroundImage.RectTransform.localScale = Vector3.one / 1.25f;
            _contentImage.RectTransform.localScale = Vector3.one / 1.25f;

            StartCoroutine(MoveUnit(gridContent, startPosition, endPosition, blowUpInEnd));
        }

        private IEnumerator MoveUnit(GridContent gridContent, Vector2 startPosition, Vector2 endPosition, bool blowUpInEnd)
        {
            yield return _moveDelay;

            AudioController.Instance.PlaySound(SoundItem.END_FLYING);

            _rectTransform.anchoredPosition = startPosition;
            Show(gridContent);
            _backgroundImage.RectTransform.DOScale(1, 0.5f);
            _contentImage.RectTransform.DOScale(1, 0.5f).OnComplete(() => _canMove = true);
            
            yield return _scaleDelay;

            if (blowUpInEnd == false)
                _rectTransform.DOAnchorPos(endPosition, GetDuration(startPosition, endPosition)).SetEase(Ease.Linear).OnComplete(Hide);
            else
                _rectTransform.DOAnchorPos(endPosition, GetDuration(startPosition, endPosition)).SetEase(Ease.Linear).OnComplete(HideAndBoom);
        }

        private void Show(GridContent gridContent)
        {
            _contentImage.SetImage(gridContent.SymbloSprite, gridContent.SymbolColor);
            _backgroundImage.SetImage(gridContent.BackgroundSprite, gridContent.BackgroundColor);
        }

        private void Hide()
        {
            _contentImage.ResetImage();
            _backgroundImage.ResetImage();

            AudioController.Instance.PlaySound(SoundItem.CHIP_COLLIDE);
            OnEndMoving?.Invoke();
        }

        private void HideAndBoom()
        {
            _boomMainModule.startColor = _backgroundImage.Color;
            _boom.Play();
            Hide();
        }

        #region HelperFunction
        private float GetDuration(Vector2 startPosition, Vector2 endPosition)
            => Vector2.Distance(startPosition, endPosition) / 100 * _speed;

        #endregion
    }
}

