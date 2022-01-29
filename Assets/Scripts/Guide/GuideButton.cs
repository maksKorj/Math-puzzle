using LevelBuilder;
using System;
using UnityEngine;

public class GuideButton : MonoBehaviour
{
    private RectTransform _rectTransform;
    private GuideHandler _guideHandler;

    private Action GridButtonAction;

    public void SetPosition(GridButton gridButton, GuideHandler guideHandler)
    {
        if(_rectTransform == null)
        {
            _guideHandler = guideHandler;
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = gridButton.Size;
        }

        _rectTransform.anchoredPosition = gridButton.WorldPosition;
        GridButtonAction = gridButton.MoveUnit;
    }

    public void Click()
    {
        GridButtonAction();
        _guideHandler.Hide();
    }

}
