using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfigs/Symbol Config/All symbols")]
public class Symbols : ScriptableObject
{
    [SerializeField] private Number[] _numbers;
    [SerializeField] private MathSign[] _mathSigns;
    [SerializeField] private ComparisonSign[] _comparisonSigns;

    public Number[] Numbers => _numbers;
    public MathSign[] MathSigns => _mathSigns;
    public ComparisonSign[] ComparisonSigns => _comparisonSigns;
}
