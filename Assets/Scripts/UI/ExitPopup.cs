using UnityEngine;

public class ExitPopup : MonoBehaviour
{
    [SerializeField] private PopUpAnimation _popUpAnimation;

    private void Start()
        => BackButton.Instance.SetDefaultAction(Open);

    private void Open()
    {
        _popUpAnimation.Open();
        BackButton.Instance.AddBackButtonAction(_popUpAnimation.Close);
    }

    public void ClosePopUp()
    {
        _popUpAnimation.Close();
        BackButton.Instance.RemoveLastAction();
    }

    public void Exit() => Application.Quit();
}
