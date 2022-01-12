using System;
using UnityEngine;

public class SymbolGiver : MonoBehaviour
{
    [SerializeField] private SymbolConfig _symbolConfig;
    [SerializeField] private SymbolGiverVisual _symbolGiverVisual;
    [SerializeField] private EquationVisualizer _equationVisualizer;
    [SerializeField] private Symbols _symbols;

    private int _level;
    private int _count = 0;
    private GridContent _currentGridContent;
    private Action SettingCharacter;
    private SmartSymbolGiver _smartSymbolGiver;

    public SymbolGiverVisual SymbolGiverVisual => _symbolGiverVisual;

    private void Awake()
    {
        _smartSymbolGiver = GetComponent<SmartSymbolGiver>();

        _level = PlayerSaver.LoadPlayerLevel();
        SetOutput();

        SetCharacter();
        _equationVisualizer.OnEndChecking += SetCharacter;
    }

    private void OnDisable() 
        => _equationVisualizer.OnEndChecking -= SetCharacter;

    public GridContent GetNextSymbol() => _currentGridContent;

    public void SetCharacter() => SettingCharacter();

    private void WithoutRandomOutput()
    {
        int index = _count % _symbolConfig.GetLevelSymbols(_level).GridContentSize;
        _currentGridContent = _symbolConfig.GetLevelSymbols(_level).GridContent(index);
        _count++;

        _symbolGiverVisual.ShowSymbol(_currentGridContent);
    }

    private void RandomOutput()
    {
        _currentGridContent = GetRandomGridContent();

        /*if (UnityEngine.Random.Range(0f, 100f) > 70f)
            _currentGridContent = GetRandomGridContent();
        else
            _currentGridContent = _smartSymbolGiver.GetElement();*/

        _symbolGiverVisual.ShowSymbol(_currentGridContent);
    }

    private GridContent GetRandomGridContent()
        => _symbols.AllSymbols[UnityEngine.Random.Range(0, _symbols.AllSymbols.Length)];

    private void SetOutput()
    {
        if (_symbolConfig.GetLevelSymbols(_level).IsRandomOutput == false)
            SettingCharacter = WithoutRandomOutput;
        else
        {
            SettingCharacter = RandomOutput;
            _symbols.AllSymbols.Shuffle();
        }    
    }

}
