using UnityEngine;
using TMPro;
using StartMenu.BoosterUi;

namespace StartMenu
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelDisplay;
        [SerializeField] private Lives _lives;
        [SerializeField] private BoosterSelector _boosterSelector;
        [SerializeField] private LifeShopPopUp _lifeShopPopUp;
        [SerializeField] private TabletScaler _tabletScaler;

        private string _levelName;

        private void Awake()
        {
            _tabletScaler.CheckResolution();
            _levelName = $"Level {PlayerSaver.LoadPlayerLevel()}";
            _levelDisplay.text = _levelName;
        }

        public void StartGame()
        {
            if(_lives.HasLives)
                _boosterSelector.Open(_levelName);
            else
            {
                _lifeShopPopUp.Open();
            }
        }
    }
}

