using UnityEngine;
using TMPro;

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

    public void ShowOpenButton() => _openButton.SetActive(true);
    public void HideOpenButton() => _openButton.SetActive(false);
}
