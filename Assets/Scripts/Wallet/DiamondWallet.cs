
public class DiamondWallet : Wallet
{
    protected override void GetAmount() => Amount = PlayerSaver.LoadPlayerDiamonds();
    protected override void SaveAmount(int amount) => PlayerSaver.SavePlayerDiamonds(amount);
}
