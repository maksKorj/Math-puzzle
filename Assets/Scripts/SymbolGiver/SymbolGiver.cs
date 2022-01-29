using System;
using System.Collections.Generic;
using UnityEngine;

public class SymbolGiver : MonoBehaviour
{
    [SerializeField] private LevelPropertyHandler _levelPropertyHandler;
    [SerializeField] private SymbolGiverVisual _symbolGiverVisual;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private Symbols _symbols;

    private GridContent _currentGridContent;
    private Action SettingCharacter;
    private SmartSymbolGiver _smartSymbolGiver;
    private SymbolGiverHelper _symbolGiverHelper;
    private List<GridContent> _preparedGridContents;

    public SymbolGiverVisual SymbolGiverVisual => _symbolGiverVisual;

    public GridContent GetNextSymbol() => _currentGridContent;
    public void SetCharacter() => SettingCharacter();

    private void Awake()
    {
        _smartSymbolGiver = GetComponent<SmartSymbolGiver>();
        
        _symbolGiverHelper = new SymbolGiverHelper(
            new Amount(3, _symbols.Numbers),
            new Amount(1, _symbols.MathSigns),
            new Amount(1, _symbols.ComparisonSigns));

        SetOutput();

        _equationVisualizer.OnEndChecking += SetCharacter;
    }

    private void OnDisable() 
        => _equationVisualizer.OnEndChecking -= SetCharacter;

    public void ChangeSymbol(GridContent gridContent)
    {
        _currentGridContent = gridContent;
        _symbolGiverVisual.ChangeSymbol(_currentGridContent);
    }

    public void ChangeSymbol()
    {
        _symbolGiverVisual.ChangeSymbol(SetCharacter);
    }

    private void SetOutput()
    {
        if (_levelPropertyHandler.HasSymbolConfig())
        {
            SetPreparedList();

            if (_levelPropertyHandler.SymbolConfig().IsRandomOutput == false)
                SettingCharacter = PreparedWithoutRandomOutput;
            else
                SettingCharacter = PreparedRandomOutput;
            
            _equationVisualizer.OnEmptyGrid += ChangeToUnpreparedRandom;
        }   
        else
            SettingCharacter = RandomOutput;
    }

    #region PreparedOutput
    private void SetPreparedList()
    {
        _preparedGridContents = new List<GridContent>();

        for (int i = 0; i < _levelPropertyHandler.SymbolConfig().GridContentAmount; i++)
            _preparedGridContents.Add(_levelPropertyHandler.SymbolConfig().GridContent(i));
    }

    private void PreparedWithoutRandomOutput()
    {
        if (_symbolGiverVisual.IsEmpty() == false)
            return;

        _currentGridContent = _preparedGridContents[0];
        _preparedGridContents.RemoveAt(0);

        _symbolGiverVisual.ShowSymbol(_currentGridContent);

        if (_preparedGridContents.Count <= 0)
            ChangeToUnpreparedRandom();
    }

    private void PreparedRandomOutput()
    {
        if (_symbolGiverVisual.IsEmpty() == false)
            return;

        int index = UnityEngine.Random.Range(0, _preparedGridContents.Count);
        _currentGridContent = _preparedGridContents[index];
        _preparedGridContents.RemoveAt(index);

        _symbolGiverVisual.ShowSymbol(_currentGridContent);

        if (_preparedGridContents.Count <= 0)
            ChangeToUnpreparedRandom();
    }

    private void ChangeToUnpreparedRandom()
    {
        SettingCharacter = RandomOutput;
        _equationVisualizer.OnEmptyGrid -= ChangeToUnpreparedRandom;
    }
    #endregion

    private void RandomOutput()
    {
        if (_symbolGiverVisual.IsEmpty() == false)
            return;

        if (UnityEngine.Random.Range(0f, 100f) > 90f)
            _currentGridContent = _symbolGiverHelper.GetGridContent();
        else
        {
            var content = _smartSymbolGiver.GetElement();

            if (content is Number == false)
            {
                if (IsRepeatedSign(content))
                    content = _symbolGiverHelper.GetGridContent();
            }

            _currentGridContent = content;
            _symbolGiverHelper.AddToAmount(_currentGridContent.GetType());
        }

        _symbolGiverVisual.ShowSymbol(_currentGridContent);
    }

    private bool IsRepeatedSign(GridContent content) => content is MathSign && _currentGridContent is MathSign
        || content is ComparisonSign && _currentGridContent is ComparisonSign;
}

public class SymbolGiverHelper
{
    private Amount[] _amounts = new Amount[3];
    private int _startIndex;

    public SymbolGiverHelper(Amount numberAmount, Amount mathSignAmount, Amount comparsionSignAmount)
    {
        _amounts[0] = numberAmount;
        _amounts[1] = mathSignAmount;
        _amounts[2] = comparsionSignAmount;
    }

    public void AddToAmount(Type type)
    {
        for(int i = 0; i < _amounts.Length; i++)
        {
            if(_amounts[i].ContentType == type)
            {
                _amounts[i].Add();
                return;
            }
        }
    }

    public GridContent GetGridContent(int index = -1)
    {
        GridContent gridContent;

        if(index < 0)
        {
            _startIndex = UnityEngine.Random.Range(0, _amounts.Length);

            if (_amounts[_startIndex].CanGetContent(out gridContent))
                return gridContent;
            else
                return GetGridContent(NextIndex(_startIndex));
        }
        else
        {
            if(index == _startIndex)
            {
                ResetAll();
                _startIndex = UnityEngine.Random.Range(0, _amounts.Length);
                _amounts[_startIndex].CanGetContent(out gridContent);
                
                return gridContent;
            }

            if (_amounts[index].CanGetContent(out gridContent))
                return gridContent;
            else
                return GetGridContent(NextIndex(index));
        }
    }

    private int NextIndex(int currentIndex)
        => (currentIndex + 1) % _amounts.Length;

    private void ResetAll()
    {
        for (int i = 0; i < _amounts.Length; i++)
            _amounts[i].ResetAmount();
    }
}

public class Amount
{
    private readonly int _maxAmount;
    private int _currentAmount = 0;
    private GridContent[] _gridContents;

    public Amount(int maxAmount, GridContent[] gridContent)
    {
        _maxAmount = maxAmount + 1;
        _gridContents = gridContent;
    }

    public Type ContentType => _gridContents[0].GetType();

    public bool CanGetContent(out GridContent gridContent)
    {
        _currentAmount++;

        if(_currentAmount < _maxAmount)
        {
            gridContent = _gridContents[UnityEngine.Random.Range(0, _gridContents.Length)];
            return true;
        }

        gridContent = null;
        return false;
    }

    public void Add() => _currentAmount++;

    public void ResetAmount() => _currentAmount = 0;
}

