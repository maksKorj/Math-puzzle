using UnityEngine;

public class MainShop : MonoBehaviour
{
    [SerializeField] private MainShopTab[] _mainShopTabs;
    [SerializeField] private GameObject _childe;
    [SerializeField] private MainWallet _wallet;

    public TabHelper TabHelper { get; private set; }

    private void Awake()
    {
        for (int i = 0; i < _mainShopTabs.Length; i++)
            _mainShopTabs[i].Initialize(this);

        TabHelper = GetComponent<TabHelper>();

        _childe.SetActive(false);
    }

    public void UnSelectAll()
    {
        for (int i = 0; i < _mainShopTabs.Length; i++)
            _mainShopTabs[i].UnSelect();
    }

    public void OpenShop() => OpenTab(0);
    public void OpenGemTab() => OpenTab(1);

    private void OpenTab(int index)
    {
        _childe.SetActive(true);
        _wallet.HideOpenButtons();

        _mainShopTabs[index].Select();
    }

    public void CloseShop()
    {
        _childe.SetActive(false);
        _wallet.ShowOpenButtons();
    }
}
