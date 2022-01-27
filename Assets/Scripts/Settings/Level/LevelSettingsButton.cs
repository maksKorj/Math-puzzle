using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettingsButton : MonoBehaviour
{
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private SettingItem _settingItem;

    private Image _image;
    private Action DoAction;

    private void OnEnable() => UpdateButton();

    public void Click() => DoAction();

    private void UpdateButton()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        float value = SettingsSaver.LoadValue(_settingItem);
        if (value > 0)
            SetUnMuteValue();
        else
            SetMuteValue();
    }

    private void SetMuteValue()
    {
        _image.sprite = _muteSprite;
        DoAction = UnMute;
    }

    private void SetUnMuteValue()
    {
        _image.sprite = _activeSprite;
        DoAction = Mute;
    }

    private void Mute()
    {
        //ToDoSound;
        SettingsSaver.SaveSettingItemValue(_settingItem, 0f);
        SetMuteValue();
    }

    private void UnMute()
    {
        //ToDO
        SettingsSaver.SaveSettingItemValue(_settingItem, 0.5f);
        SetUnMuteValue();
    }

}
