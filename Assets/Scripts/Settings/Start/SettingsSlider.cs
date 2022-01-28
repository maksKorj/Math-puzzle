using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    [SerializeField] private SettingItem _settingItem;
    [Header("Sprites")]
    [SerializeField] private Sprite _muteIcon;
    [SerializeField] private Sprite _activeIcon;
    [Header("Elements")]
    [SerializeField] private Image _imageIcon;
    [SerializeField] private Slider _slider;

    private void OnEnable() => _slider.value = SettingsSaver.LoadValue(_settingItem);
    private void OnDisable() => SettingsSaver.SaveSettingItemValue(_settingItem, _slider.value);

    public void UpdateValue()
    {
        AudioController.Instance.SetAudioValue(_settingItem, _slider.value);

        if (_slider.value == 0)
            _imageIcon.sprite = _muteIcon;
        else
            _imageIcon.sprite = _activeIcon;
    }
}
