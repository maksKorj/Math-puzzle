using System;
using UnityEngine;

public class SymbolGiver : MonoBehaviour
{
    [SerializeField] private SymbolConfig _symbolConfig;
    [SerializeField] private SymbolGiverVisual _symbolGiverVisual;
    [SerializeField] private EquationVisualizer _equationVisualizer;

    private int _level;
    private int _count = 0;
    private GridContent _currentGridContent;
    private Action SettingCharacter;

    public SymbolGiverVisual SymbolGiverVisual => _symbolGiverVisual;

    private void Awake()
    {
        _level = PlayerSaver.LoadPlayerLevel();
        SetCharacter();
        _equationVisualizer.OnEndChecking += SetCharacter;


    }

    private void OnDisable() 
        => _equationVisualizer.OnEndChecking -= SetCharacter;

    public GridContent GetNextSymbol() => _currentGridContent;

    public void SetCharacter()
    {
        SettingCharacter();
        /*if (_symbolConfig.GeTLevelSymbols(_level).IsRandomOutput == false)
        {
            
        }*/
    }

    private void WithoutRandomOutput()
    {
        int index = _count % _symbolConfig.GetLevelSymbols(_level).GridContentSize;
        _currentGridContent = _symbolConfig.GetLevelSymbols(_level).GridContent(index);
        _count++;

        _symbolGiverVisual.ShowSymbol(_currentGridContent);
    }

    private void RandomOutputWithSymbolList()
    {
        _currentGridContent = _symbolConfig.GetLevelSymbols(_level).GridContent(UnityEngine.Random.Range(0, _symbolConfig.GetLevelSymbols(_level).GridContentSize));
    }

}
