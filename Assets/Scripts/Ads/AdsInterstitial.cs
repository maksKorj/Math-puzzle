using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsInterstitial : MonoBehaviour
{
    [SerializeField] private string _interstitialUnitId = "ca-app-pub-3940256099942544/1033173712";

    private InterstitialAd _interstitialAd;
    private int _count = 0;
    private DateTime _startDate;
    private TimeSpan _fiveMinutes = new TimeSpan(0, 5, 0);

    public Action OnAdsClosed;

    void Start()
    {
        _interstitialAd = new InterstitialAd(_interstitialUnitId);
        _interstitialAd.LoadAd(GetRequest());

        _interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        OnAdsClosed?.Invoke();
        _interstitialAd.LoadAd(GetRequest());
    }

    private AdRequest GetRequest() => new AdRequest.Builder().Build();

    public void ShowAd()
    {
        _count++;

        Debug.Log("Count: " + _count + " Total time: " + (DateTime.Now - _startDate));
        if(_count > 1 || DateTime.Now - _startDate >= _fiveMinutes)
        {
            _count = 0;

            if (_interstitialAd.IsLoaded())
            {
                _interstitialAd.Show();
            }
            else
            {
                OnAdsClosed?.Invoke();
            }

            return;
        }

        OnAdsClosed?.Invoke();
    }

    public void SetTime() => _startDate = DateTime.Now;
}
