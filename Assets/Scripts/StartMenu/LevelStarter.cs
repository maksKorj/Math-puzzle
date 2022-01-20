using UnityEngine;
using TMPro;

namespace StartMenu
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelDisplay;
        [SerializeField] private Lives _lives;
        [SerializeField] private BoosterSelector _boosterSelector;

        private string _levelName;

        private void Awake()
        {
            _levelName = $"Level {PlayerSaver.LoadPlayerLevel()}";
            _levelDisplay.text = _levelName;
        }

        public void StartGame()
        {
            if(_lives.HasLives)
                _boosterSelector.Open(_levelName);
            else
            {
                //open Lives Pop Up
            }
        }
    }
}

