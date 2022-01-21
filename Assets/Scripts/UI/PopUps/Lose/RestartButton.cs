using UnityEngine;
using StartMenu;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Lives _lives;
    [SerializeField] private LifeShopPopUp _lifeShopPopUp;

    public void Restart()
    {
        if (_lives.HasLives)
            _levelLoader.LoadLevel();
        else
            _lifeShopPopUp.Open();
    }
} 
