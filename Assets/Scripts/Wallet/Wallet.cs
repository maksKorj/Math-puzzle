using UnityEngine;
using TMPro;
using DG.Tweening;

public abstract class Wallet : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _textDisplay;
    [SerializeField] private GameObject _openButton;

    private int _amount = -1;
    protected int Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = value;
            _textDisplay.text = _amount.ToString();
            SaveAmount(_amount);
        }
    }

    private void Awake() => GetAmount();

    protected abstract void GetAmount();
    protected abstract void SaveAmount(int amount);

    public virtual void Add(int amount)
    {
        if (_amount < 0)
            GetAmount();

        Amount += amount;
    }

    public bool CanBuy(int amount)
    {
        int remainingAmount = Amount - amount;

        if (remainingAmount >= 0)
        {
            Amount = remainingAmount;
            return true;
        }

        return false;
    }

    public bool CanShowBuyButton(int amount)
    {
        if (_amount < 0)
            GetAmount();

        return Amount >= amount;
    }

    private RectTransform _openButtonTransform;

    public void ShowOpenButton()
    {
        _openButton.SetActive(true);
        _openButtonTransform.DOScale(Vector2.one, 0.3f).SetEase(Ease.OutBounce);
    }

    public void HideOpenButton()
    {
        if (_openButtonTransform == null)
            _openButtonTransform = _openButton.GetComponent<RectTransform>();

        _openButtonTransform.DOScale(Vector2.zero, 0.4f)
            .SetEase(Ease.InCirc).OnComplete(() => _openButton.SetActive(false));
    }
}
