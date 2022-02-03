using UnityEngine;

public class MoveShopPopUp : MonoBehaviour
{
    [SerializeField] private MoveShopButton[] _moveShopButtons;
    [SerializeField] private PopUpAnimationAditional _popUpAnimationAditional;

    private void Awake()
    {
        for (int i = 0; i < _moveShopButtons.Length; i++)
            _moveShopButtons[i].Initialize();
    }

    public void OpenShop()
    {
        _popUpAnimationAditional.Open();
        BackButton.Instance.AddBackButtonAction(_popUpAnimationAditional.Close);
    }

    public void CloseShop()
    {
        _popUpAnimationAditional.Close();
        BackButton.Instance.RemoveLastAction();
    }
}
