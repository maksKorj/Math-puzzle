
public class CoinWallet : Wallet
{
    protected override void GetAmount() => Amount = PlayerSaver.LoadPlayerCoins();
    protected override void SaveAmount(int amount) => PlayerSaver.SavePlayerCoins(amount);
}
