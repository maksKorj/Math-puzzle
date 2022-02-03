using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private PopUpAnimation _popUpAnimation;
    [SerializeField] private GameObject _selectLanguagePopup;

    public void OpenSettings()
    {
        _popUpAnimation.Open();
        BackButton.Instance.AddBackButtonAction(_popUpAnimation.Close);
    }

    public void CloseSettings()
    {
        _popUpAnimation.Close();
        BackButton.Instance.RemoveLastAction();
    }

    public void OpenSelectLanguagePopup() => _selectLanguagePopup.SetActive(true);
    public void CloseSelectLanguagePopup() => _selectLanguagePopup.SetActive(false);
}
