using UnityEngine;

public class SettingsPopup : MonoBehaviour
{
    [SerializeField] private PopUpAnimation _popUpAnimation;
    [SerializeField] private GameObject _selectLanguagePopup;

    public void OpenSettings() => _popUpAnimation.Open();
    public void CloseSettings() => _popUpAnimation.Close();

    public void OpenSelectLanguagePopup() => _selectLanguagePopup.SetActive(true);
    public void CloseSelectLanguagePopup() => _selectLanguagePopup.SetActive(false);
}
