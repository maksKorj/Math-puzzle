using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SymbolGiverParticle : MonoBehaviour
{
    [SerializeField] private RectTransform _symbolGiverTransform;
    [SerializeField] private SymbolGiverVisual _symbolGiverVisual;
    [SerializeField] private float _time = 0.25f;

    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _particleMainModule;
    private RectTransform _rectTransform;

    private Vector2 _startPosition;

    public bool IsMovingToPosition { get; private set; }

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _particleMainModule = _particleSystem.main;

        _rectTransform = GetComponent<RectTransform>();
        _rectTransform.position = _startPosition;
    }

    private void Start() => transform.SetAsLastSibling();

    public void MoveTo(Vector2 endPosition, Color color)
    {
        IsMovingToPosition = true;

        if(_startPosition == Vector2.zero)
            _startPosition = _symbolGiverTransform.position;

        _rectTransform.position = _startPosition;
        _symbolGiverVisual.Hide();
        StartCoroutine(Move(endPosition, color));
    }

    private IEnumerator Move(Vector2 endPosition, Color color)
    {
        yield return new WaitUntil(() => _symbolGiverVisual.IsFading == false);

        _particleMainModule.startColor = color;
        _particleSystem.Play();

        _rectTransform.DOAnchorPos(endPosition, GetDuration(_rectTransform.anchoredPosition, endPosition))
            .SetEase(Ease.Linear)
            .OnComplete(StopParticle);
    }

    private void StopParticle()
    {
        IsMovingToPosition = false;

        if (_particleSystem.isPlaying)
            _particleSystem.Stop();
    }

    private float GetDuration(Vector2 startPosition, Vector2 endPosition)
            => Vector2.Distance(startPosition, endPosition) / 100 * _time;
}
