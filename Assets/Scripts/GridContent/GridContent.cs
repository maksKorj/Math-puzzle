using UnityEngine;

public abstract class GridContent : ScriptableObject
{
    [SerializeField] protected Sprite _backgroundSprite;
    [SerializeField] protected Sprite _sumbolSprite;
    [SerializeField] protected Color _backgroundColor = Color.white;
    [SerializeField] protected Color _symbolColor = Color.white;

    public Sprite BackgroundSprite => _backgroundSprite;
    public Sprite SymbloSprite => _sumbolSprite;
    public Color BackgroundColor => _backgroundColor;
    public Color SymbolColor => _symbolColor;
}
