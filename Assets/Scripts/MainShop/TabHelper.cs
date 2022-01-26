using UnityEngine;

public class TabHelper : MonoBehaviour
{
    [SerializeField] private TabSizeHandler _defaultTabSizeHandler;
    [SerializeField] private TabSizeHandler _activeTabSizeHandler;
    [SerializeField] private TabImageHandler _backgroundSprite;
    [SerializeField] private Color _defaultTextColor = new Color(1, 1, 1, 1);

    public TabSizeHandler DefaultTabSizeHandler => _defaultTabSizeHandler;
    public TabSizeHandler ActiveTabSizeHandler => _activeTabSizeHandler;
    public TabImageHandler BackgroundSprite => _backgroundSprite;
    public Color DefaultTextColor => _defaultTextColor;
}

[System.Serializable]
public class TabSizeHandler
{
    [SerializeField] private int _y;
    [SerializeField] private Vector3 _size;

    public int PositionY => _y;
    public Vector3 Size => _size;
}

[System.Serializable]
public class TabImageHandler
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _activeSprite;

    public Sprite DefaultSprite => _defaultSprite;
    public Sprite ActiveSprite => _activeSprite;
}