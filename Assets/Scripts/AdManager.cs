using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class AdManager : MonoBehaviour
{
    public Text bannerAdsText;
    public Text interstitialAdsText;
    public Text rewardedAdsText;

    private void Awake()
    {
        if (RuntimeManager.IsInitialized())
        {
            RuntimeManager.Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Advertising.IsInterstitialAdReady())
        {
            interstitialAdsText.text = "Interstitial ads ready";
            interstitialAdsText.color = Color.green;
        }
        else
        {
            interstitialAdsText.text = "Interstitial ads not ready";
            interstitialAdsText.color = Color.red;
        }

        if (Advertising.IsRewardedAdReady())
        {
            rewardedAdsText.text = "Rewarded ads ready";
            rewardedAdsText.color = Color.green;
        }
        else
        {
            rewardedAdsText.text = "Rewarded ads not ready";
            rewardedAdsText.color = Color.red;
        }

    }

    public void ShowBannerAds()
    {
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }

    public void ShowInterstitialAds()
    {
        if (Advertising.IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd();
        }
    }

    public void ShowRewardedAds()
    {
        if (Advertising.IsRewardedAdReady())
        {
            Advertising.ShowRewardedAd();
        }
    }
}
