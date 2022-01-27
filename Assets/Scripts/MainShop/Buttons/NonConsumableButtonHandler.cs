using UnityEngine;

public class NonConsumableButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _adBlockButton, _infiniteLivesButton, _specialButton;
    [SerializeField] private GameObject _text;

    private RectTransform _infiniteLivesButtonRect;

    private void OnEnable()
    {
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        if(StateSaver.IsAdBlockTurnOn())
        {
            _adBlockButton.SetActive(false);
            _specialButton.SetActive(false);
        }

        if(StateSaver.IsInfiniteLives())
        {
            _infiniteLivesButton.SetActive(false);
            _specialButton.SetActive(false);
        }
        else
        {
            if (_adBlockButton.activeInHierarchy == false)
            {
                if (_infiniteLivesButtonRect == null)
                    _infiniteLivesButtonRect = _infiniteLivesButton.GetComponent<RectTransform>();

                _infiniteLivesButtonRect.anchoredPosition = new Vector2(
                    _infiniteLivesButtonRect.anchoredPosition.x * (-1), _infiniteLivesButtonRect.anchoredPosition.y);
            }  
        }

        if(_infiniteLivesButton.activeInHierarchy == false
            && _adBlockButton.activeInHierarchy == false)
        {
            _text.gameObject.SetActive(true);
        }
    }
}
