using UnityEngine;

public class MoveShopPopUp : ShopPopUp
{
    [SerializeField] private MoveShopButton[] _moveShopButtons;

    private void Awake()
    {
        for (int i = 0; i < _moveShopButtons.Length; i++)
            _moveShopButtons[i].Initialize();

        gameObject.SetActive(false);
    }
}
