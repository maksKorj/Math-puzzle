using UnityEngine;
using StartMenu;

public class LevelSettingPopUpButton : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private Lives _lives;

    public void Quit()
    {
        _lives.ShowAndRemoveLife();
        _levelLoader.LoadStartMenu();
    }

    public void Restart()
    {
        _lives.ShowAndRemoveLife();
        if(_lives.HasLives)
            _levelLoader.LoadLevel();
        else
            _levelLoader.LoadStartMenu();
    }
}
