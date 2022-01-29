using DG.Tweening;
using System.Collections;
using UnityEngine;

public class LabelNotAvailableAnimation : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Vector2 _startPosition;

    public void PlayAnimation()
    {
        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.anchoredPosition;
        }

        if (gameObject.activeInHierarchy)
            return;

        gameObject.SetActive(true);

        _rectTransform.DOScale(Vector3.one, 0.5f).OnComplete(() => StartCoroutine(WaitAndScale()) );
        _rectTransform.DOAnchorPosY(_startPosition.y + 30, 1f);
    }

    private IEnumerator WaitAndScale()
    {
        yield return new WaitForSeconds(1f);
        _rectTransform.DOScale(Vector3.zero, 0.25f).OnComplete(SetDefault);
    }

    private void SetDefault()
    {
        gameObject.SetActive(false);
        _rectTransform.anchoredPosition = _startPosition;
    }
}
