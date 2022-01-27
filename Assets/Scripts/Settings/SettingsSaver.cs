using UnityEngine;

public static class SettingsSaver
{
    public static float LoadValue(SettingItem settingItem) => LoadSave(settingItem);
    public static void SaveSettingItemValue(SettingItem settingItem, float value) 
        => PlayerPrefs.SetFloat(GetPath(settingItem), value);
    
    private static float LoadSave(SettingItem settingItem)
    {
        var path = GetPath(settingItem);

        if (PlayerPrefs.HasKey(path))
            return PlayerPrefs.GetFloat(path);
        else
            return 0.5f;
    }

    private static string GetPath(SettingItem settingItem)
    {
        switch(settingItem)
        {
            case SettingItem.Sound:
                return "SettingSound";
            case SettingItem.Music:
                return "SettingMusic";
        }

        Debug.LogError("Unexpected value");
        return null;
    }
}

public enum SettingItem
{
    Sound,
    Music
}