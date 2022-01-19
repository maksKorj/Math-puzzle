using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class BoosterSaver
{
    private const string _boosterSaverPath = "/BoosterSaver.data";

    public static void SaveBoosters(List<BoosterItem> boosterItems, BoosterSlot[] boosterSlots)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + _boosterSaverPath;

        FileStream stream = new FileStream(path, FileMode.Create);

        SavingBoosterHandler savingBoosters = new SavingBoosterHandler(GetSavingBoosterItems(boosterItems), boosterSlots);

        formatter.Serialize(stream, savingBoosters);
        stream.Close();
    }

    private static List<SavingBoosterItem> GetSavingBoosterItems(List<BoosterItem> boosterItems)
    {
        var savingBoosterItem = new List<SavingBoosterItem>();

        for (int i = 0; i < boosterItems.Count; i++)
            savingBoosterItem.Add(new SavingBoosterItem(boosterItems[i].Booster.GetType(), boosterItems[i].Amount));

        return savingBoosterItem;
    }

    public static SavingBoosterHandler LoadBoosters()
    {
        string path = Application.persistentDataPath + _boosterSaverPath;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavingBoosterHandler boosteItems = formatter.Deserialize(stream) as SavingBoosterHandler;
            stream.Close();

            return boosteItems;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteBoosters()
    {
        string path1 = Application.persistentDataPath + _boosterSaverPath;
        File.Delete(path1);
    }
}

[Serializable]
public class SavingBoosterHandler
{
    public List<SavingBoosterItem> SavingBoosterItems { get; private set; }
    public BoosterSlot[] BoosterSlots { get; private set; }

    public SavingBoosterHandler(List<SavingBoosterItem> savingBoosterItems, BoosterSlot[] boosterSlots)
    {
        SavingBoosterItems = savingBoosterItems;
        BoosterSlots = boosterSlots;
    }
}

[Serializable]
public class SavingBoosterItem
{
    public Type BoosterType { get; private set; }
    public int Amount { get; private set; }

    public SavingBoosterItem(Type boosterType, int amount)
    {
        BoosterType = boosterType;
        Amount = amount;
    }
}

[Serializable]
public class BoosterSlot
{
    public bool HasBooster => BoosterType != null && Amount > 0;
    public Type BoosterType { get; private set; }
    public int Amount { get; private set; }

    public void ChangeBooster(BoosterItem boosterItem)
    {
        BoosterType = boosterItem.BoosterType;
        Amount = boosterItem.Amount;
    }
}

