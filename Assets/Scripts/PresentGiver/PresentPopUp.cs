using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class PresentPopUp : MonoBehaviour
{
    [Header("Coins")]
    [SerializeField] private Present _coinPresent;
    [SerializeField] private CoinWallet _coinWallet;
    [Header("Diamonds")]
    [SerializeField] private Present _diamondPresent;
    [SerializeField] private DiamondWallet _diamondWallet;
    [Header("Visual")]
    [SerializeField] private PresentVisual _walletPresent;
    [SerializeField] private PresentVisual _boosterPresent;
    [SerializeField] private GameObject _present;
    [SerializeField] private GameObject _button;
    [Header("ProgressBar")]
    [SerializeField] private GameObject _progressBarBlock;

    private Action Give;
    private int _amount;
    private BoosterItem _boosterItem;

    public void Open()
    {
        gameObject.SetActive(true);
        SetPresent();
    }

    private void SetPresent()
    {
        float percent = Random.Range(0f, 100f);

        switch (percent)
        {
            case float number when (number >= 0 && number <= 50):
                SetCoins();
                break;
            case float number when (number > 50 && number <= 75):
                SetDiamonds();
                break;
            case float number when (number > 75 && number <= 100):
                SetBooster();
                break;
        }

        _button.SetActive(true);
    }

    private void SetCoins()
    {
        _amount = _coinPresent.Amount;
        _walletPresent.ShowPresent(_coinPresent.Sprite, $"+ {_amount}");
        Give = () => _coinWallet.Add(_amount);
    }

    private void SetDiamonds()
    {
        _amount = _diamondPresent.Amount;
        _walletPresent.ShowPresent(_diamondPresent.Sprite, $"+ {_amount}");
        Give = () => _diamondWallet.Add(_amount);
    }

    private void SetBooster()
    {
        var list = BoosterSaverManager.Instance.GetAvailableBoosterItems(PlayerSaver.LoadPlayerLevel() - 1);

        if (list.Count > 0)
        {
            _amount = Random.Range(1, 6);
            _boosterItem = list[Random.Range(0, list.Count)];
            _boosterPresent.ShowPresent(_boosterItem.Booster.BoosterImage, $"x{_amount}");
            Give = () => _boosterItem.AddAmount(_amount);
        }
        else
            SetDiamonds();
    }

    public void GivePresent()
    {
        _progressBarBlock.SetActive(false);
        Give();
        gameObject.SetActive(false);
    }
}
