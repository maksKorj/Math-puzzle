using UnityEngine;

[RequireComponent(typeof(PopUpAnimation))]
public class LevelSettingsAdditionalPopup : MonoBehaviour
{
    private PopUpAnimation _popUpAnimation;

    public void Open()
    {
        if (_popUpAnimation == null)
            _popUpAnimation = GetComponent<PopUpAnimation>();

        _popUpAnimation.Open();

        BackButton.Instance.AddBackButtonAction(_popUpAnimation.Close);
    }

    public void Close()
    {
        _popUpAnimation.Close();
        BackButton.Instance.RemoveLastAction();
    }
}
