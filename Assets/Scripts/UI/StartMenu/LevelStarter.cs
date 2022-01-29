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
        [SerializeField] private LevelLoader _levelLoader;

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
            {
                if (BoosterSaverManager.Instance.AvailableBoosterItems.Count > 0)
                    _boosterSelector.Open(_levelName);
                else
                    _levelLoader.LoadLevel();
            }  
            else
            {
                _lifeShopPopUp.Open();
            }
        }
    }
}

