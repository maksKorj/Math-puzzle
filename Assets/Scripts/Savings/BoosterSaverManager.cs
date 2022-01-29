using UnityEngine;
using Boosters;
using System;
using System.Collections.Generic;

public class BoosterSaverManager : MonoBehaviour
{
    [SerializeField] private Booster[] _boosters;

    private BoosterSlot[] _boosterSlots = new BoosterSlot[4];
    private List<BoosterItem> _boosterItems = new List<BoosterItem>();

    public List<BoosterItem> SlotItems { get; private set; } = new List<BoosterItem>();
    public List<BoosterItem> AvailableBoosterItems { get; private set; } = new List<BoosterItem>();
    public List<BoosterItem> BoosterItems => _boosterItems;

    private void Awake()
    {
        SetSingeltone();
        LoadSave();

        SetBoosterSlots();
        SetAvailableBoosterItems(PlayerSaver.LoadPlayerLevel());
    }

    #region Singeltone
    private static BoosterSaverManager _instance;
    public static BoosterSaverManager Instance => _instance;

    private void SetSingeltone()
    {
        if (_instance != null && Instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    private void LoadSave()
    {
        var savingBoosterHandler = BoosterSaver.LoadBoosters();

        if (savingBoosterHandler == null)
            SetStartValue();
        else
            ConvertData(savingBoosterHandler);
    }

    #region ProcessData
    private void SetStartValue()
    {
        for(int i = 0; i < _boosters.Length; i++)
            _boosterItems.Add(new BoosterItem(_boosters[i], 3));

        for (int i = 0; i < _boosterSlots.Length; i++)
            _boosterSlots[i] = new BoosterSlot();

        Save();
    }

    private void ConvertData(SavingBoosterHandler savingBoosterHandler)
    {
        for(int i = 0; i < savingBoosterHandler.SavingBoosterItems.Count; i++)
        {
            _boosterItems.Add(new BoosterItem(GetBooster(savingBoosterHandler.SavingBoosterItems[i].BoosterType),
                savingBoosterHandler.SavingBoosterItems[i].Amount));
        }

        _boosterSlots = savingBoosterHandler.BoosterSlots;
    }

    private Booster GetBooster(Type boosterType)
    {
        for (int i = 0; i < _boosters.Length; i++)
        {
            if (boosterType == _boosters[i].GetType())
                return _boosters[i];
        }

        Debug.LogError("Booster is not found");
        return null;
    }

    private BoosterItem GetBoosterItem(Type boosterType)
    {
        for (int i = 0; i < _boosterItems.Count; i++)
        {
            if (boosterType == _boosterItems[i].BoosterType)
                return _boosterItems[i];
        }

        Debug.LogError("BoosterIItem is not found");
        return null;
    }
    #endregion

    #region Slots
    public void ChangeSlot(int index, BoosterItem boosterItem)
    {
        _boosterSlots[index].ChangeBooster(boosterItem);
        SetBoosterSlots();
        SetAvailableBoosterItems(PlayerSaver.LoadPlayerLevel());
    }

    public void SetBoosterSlots()
    {
        SlotItems.Clear();

        for(int i = 0; i < _boosterSlots.Length; i++)
        {
            if (_boosterSlots[i].HasBooster)
                SlotItems.Add(GetBoosterItem(_boosterSlots[i].BoosterType));
            else
                SlotItems.Add(null);
        }
    }
    #endregion

    #region BoosterItems
    private void SetAvailableBoosterItems(int level)
    {
        AvailableBoosterItems.Clear();

        for(int i = 0; i < _boosterItems.Count; i++)
        {
            if (_boosterItems[i].Booster.IsAvailable(level) && IsSetOnSlots(_boosterItems[i].BoosterType) == false)
                AvailableBoosterItems.Add(_boosterItems[i]);
        }
    }

    private bool IsSetOnSlots(Type boosterType)
    {
        for (int i = 0; i < _boosterSlots.Length; i++)
        {
            if (_boosterSlots[i].BoosterType == boosterType)
                return true;
        }

        return false;
    }

    public List<BoosterItem> GetAvailableBoosterItems(int level)
    {
        var list = new List<BoosterItem>();

        for (int i = 0; i < _boosterItems.Count; i++)
        {
            if (_boosterItems[i].Booster.IsAvailable(level))
                list.Add(_boosterItems[i]);
        }

        return list;
    }
    #endregion

    #region Saving
    private void OnDestroy()
    {
        if(Instance == this)
            Save();
    }  

    private void OnApplicationQuit() => Save();

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }

    private void Save() => BoosterSaver.SaveBoosters(_boosterItems, _boosterSlots);
    #endregion
}

[Serializable]
public class BoosterItem
{
    public Booster Booster { get; private set; }
    public int Amount { get; private set; }

    public void AddAmount(int amount = -1)
    {
        if (amount > 0)
            Amount += amount;
        else
            Amount++;
    }

    public void RemoveOne() => Amount--;

    public Type BoosterType => Booster.GetType();

    public BoosterItem(Booster booster, int amount)
    {
        Booster = booster;
        Amount = amount;
    }
}