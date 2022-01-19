using UnityEngine;
using TMPro;

namespace Boosters
{
    public class BoosterButtonAmount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void ShowAmount(int amount)
        {
            gameObject.SetActive(true);
            UpdateAmount(amount);
        }

        public void UpdateAmount(int amount) => _text.text = amount.ToString();
    }
}


