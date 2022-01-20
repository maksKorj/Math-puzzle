using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Image _background;

    private readonly string _startMenu = "Start Menu", _level = "Level";

    private string _currentSceneName;
    private string _loadingSceneName;

    private void Awake()
    {
        _background.DOFade(0, 1f).OnComplete(() => _background.gameObject.SetActive(false));
        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void LoadStartMenu() => LoadScene(_startMenu);
    public void LoadLevel() => LoadScene(_level);

    private void LoadScene(string sceneName)
    {
        _loadingSceneName = sceneName;

        /*if (_currentSceneName.Equals(_level))
        {
            //AdsController.Instance.AdsInterstitial.OnAdsClosed += PlayAnimationByName;
            //AdsController.Instance.ShowInterstitialAds();
        }
        else
            PlayAnimationByName();*/

        PlayAnimationAndLoad();
    }

    private void PlayAnimationAndLoad()
    {
        //AdsController.Instance.AdsInterstitial.OnAdsClosed -= PlayAnimationByName;

        _background.gameObject.SetActive(true);
        _background.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(_loadingSceneName));
    }
}
