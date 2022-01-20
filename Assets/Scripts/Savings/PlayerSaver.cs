using UnityEngine;

public static class PlayerSaver
{
    private const string _playerLevelPath = "PlayerLevel";
    private const string _playerCoinsPath = "PlayerCoins";
    private const string _playerDiamondPath = "DiamondPath";
    private const string _playerLifePath = "PlayerLife";
    private const string _levelCompletePath = "LevelComplete";

    public static int LoadPlayerLevel() { int level = LoadSave(_playerLevelPath); return level == -1 ? 1 : level; }
    public static void SavePlayerLevel(int level) => PlayerPrefs.SetInt(_playerLevelPath, level);

    public static int LoadPlayerCoins() { int coins = LoadSave(_playerCoinsPath); return coins == -1 ? 0 : coins; }
    public static void SavePlayerCoins(int coins) => PlayerPrefs.SetInt(_playerCoinsPath, coins);
    public static int LoadPlayerDiamonds() { int diamonds = LoadSave(_playerDiamondPath); return diamonds == -1 ? 0 : diamonds; }
    public static void SavePlayerDiamonds(int diamonds) => PlayerPrefs.SetInt(_playerDiamondPath, diamonds);

    public static int LoadPlayerLives() { int lives = LoadSave(_playerLifePath); return lives == -1 ? 3 : lives; }
    public static void SavePlayerLife(int lives) => PlayerPrefs.SetInt(_playerLifePath, lives);

    public static int LoadCompleteLevelNumber() { int amount = LoadSave(_levelCompletePath); return amount == -1 ? 0 : amount; }
    public static void SaveCompleteLevelNumber(int amount) => PlayerPrefs.SetInt(_levelCompletePath, amount);

    private static int LoadSave(string path)
    {
        if (PlayerPrefs.HasKey(path))
            return PlayerPrefs.GetInt(path);
        else
            return -1;
    }
}
