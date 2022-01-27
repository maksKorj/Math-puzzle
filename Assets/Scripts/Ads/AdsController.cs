using UnityEngine;

public class AdsController : MonoBehaviour
{
    [SerializeField] private AdsInterstitial _adsInterstitial;
    [SerializeField] private AdsRewarded _adsRewarded;

    private static AdsController _instance;
    public static AdsController Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public AdsRewarded AdsRewarded => _adsRewarded;
    public AdsInterstitial AdsInterstitial => _adsInterstitial;

    public void ShowRewardedAds() => _adsRewarded.UserChoseToWatchAd();
    public void ShowInterstitialAds() => _adsInterstitial.ShowAd();
}
