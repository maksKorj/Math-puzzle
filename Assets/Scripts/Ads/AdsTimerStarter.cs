using UnityEngine;

public class AdsTimerStarter : MonoBehaviour
{
    private void Start()
        => AdsController.Instance.AdsInterstitial.SetTime();
}
