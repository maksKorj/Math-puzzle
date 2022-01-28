using UnityEngine;

public abstract class FreeButton : MonoBehaviour
{
    public virtual void GetFree()
    {
        AdsController.Instance.AdsRewarded.OnFinishWatchingAds += Reward;
        AdsController.Instance.ShowRewardedAds();
    }

    private void Reward()
    {
        AdsController.Instance.AdsRewarded.OnFinishWatchingAds -= Reward;
        Give();
    }

    protected abstract void Give();
}
