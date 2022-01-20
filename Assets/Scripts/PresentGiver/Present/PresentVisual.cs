using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PresentVisual : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amountDisplay;

    public void ShowPresent(Sprite sprite, string amount)
    {
        gameObject.SetActive(true);
        _image.sprite = sprite;
        _amountDisplay.text = amount;
    }
}
