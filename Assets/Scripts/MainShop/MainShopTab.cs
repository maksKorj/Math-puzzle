using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainShopTab : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private GameObject _childe;
    [SerializeField] private TabImageHandler _tabImageHandler;
    [SerializeField] private TextMeshProUGUI _text;

    private Image _background;
    private RectTransform _rectTransform;
    private MainShop _mainShop;

    public void Initialize(MainShop mainShop)
    {
        _background = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();

        _mainShop = mainShop;
    }

    public void Select()
    {
        if (_childe.activeInHierarchy == true)
            return;

        _mainShop.UnSelectAll();

        SetTab(_mainShop.TabHelper.ActiveTabSizeHandler, _mainShop.TabHelper.BackgroundSprite.ActiveSprite, Color.white);
        _iconImage.sprite = _tabImageHandler.ActiveSprite;

        _childe.SetActive(true);
    }

    public void UnSelect()
    {
        if (_childe.activeInHierarchy == false)
            return;

        SetTab(_mainShop.TabHelper.DefaultTabSizeHandler, _mainShop.TabHelper.BackgroundSprite.DefaultSprite,
            _mainShop.TabHelper.DefaultTextColor);
        _iconImage.sprite = _tabImageHandler.DefaultSprite;

        _childe.SetActive(false);
    }

    private void SetTab(TabSizeHandler tabSizeHandler, Sprite sprite, Color color)
    {
        _rectTransform.sizeDelta = tabSizeHandler.Size;
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, tabSizeHandler.PositionY);
        _background.sprite = sprite;

        _text.color = color;
    }
}


