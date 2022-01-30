using UnityEngine;
using TMPro;

public class CoinGiver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountDisplay;
    [SerializeField] private CoinWallet _coinWallet;

    public void UpdateAmount()
    {
        int givenAmount = PlayerSaver.LoadPlayerLevel() * 80 + 40 + Random.Range(0, 91);
        _amountDisplay.text = $"+ {givenAmount}";
        _coinWallet.Add(givenAmount);
    }
}
