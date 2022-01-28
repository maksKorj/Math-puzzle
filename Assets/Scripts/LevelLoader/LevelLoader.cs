using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Image _background;

    private readonly string _startMenu = "Start Menu", _level = "Level";

    private string _currentSceneName;
    private string _loadingSceneName;

    private bool _isAdWatched;

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

        if (_currentSceneName.Equals(_level))
        {
            _isAdWatched = false;
            StartCoroutine(WaitAndGiveLives());

            AdsController.Instance.AdsInterstitial.OnAdsClosed += OnEndWatched;
            AdsController.Instance.ShowInterstitialAds();
        }
        else
            PlayAnimationAndLoad();
    }

    private void OnEndWatched()
    {
        AdsController.Instance.AdsInterstitial.OnAdsClosed -= OnEndWatched;
        _isAdWatched = true;
    }

    private IEnumerator WaitAndGiveLives()
    {
        yield return new WaitWhile(() => _isAdWatched == false);
        PlayAnimationAndLoad();
    }

    private void PlayAnimationAndLoad()
    {
        AdsController.Instance.AdsInterstitial.OnAdsClosed -= PlayAnimationAndLoad;

        _background.gameObject.SetActive(true);
        _background.DOFade(1, 1f).OnComplete(() => SceneManager.LoadScene(_loadingSceneName));
    }
}
