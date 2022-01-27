using UnityEngine;

public static class StateSaver
{
    private const string _adBlockPath = "AdBlock";
    private const string _infiniteLivesPath = "InfiniteLives";

    public static bool IsAdBlockTurnOn() { int state = LoadSave(_adBlockPath); return state > 0; }
    public static void TurnOnAdblock() => PlayerPrefs.SetInt(_adBlockPath, 100);

    public static bool IsInfiniteLives() { int state = LoadSave(_infiniteLivesPath); return state > 0; }
    public static void SetInfiniteLives() => PlayerPrefs.SetInt(_infiniteLivesPath, 100);

    private static int LoadSave(string path)
    {
        if (PlayerPrefs.HasKey(path))
            return PlayerPrefs.GetInt(path);
        else
            return -1;
    }
}
