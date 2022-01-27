using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsRewarded : MonoBehaviour
{
    [SerializeField] private string _rewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
    
    private RewardedAd _rewardedAd;
    private bool _canBeRewarded = true;

    public Action OnFinishWatchingAds;

    void Start() => CreateAndLoadRewardedAd();

    private void CreateAndLoadRewardedAd()
    {
        _rewardedAd = new RewardedAd(_rewardedUnitId);
        _rewardedAd.LoadAd(GetRequest());

        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private AdRequest GetRequest() => new AdRequest.Builder().Build();

    private void HandleUserEarnedReward(object sender, Reward args) => _canBeRewarded = true;

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        CreateAndLoadRewardedAd();

        if(_canBeRewarded)
        {
            OnFinishWatchingAds?.Invoke();
            _canBeRewarded = false;
        }
    }

    public void UserChoseToWatchAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }
}
