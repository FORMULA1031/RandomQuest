using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    //BannerView bannerView;

    // Use this for initialization
    void Start()
    {/*
        // アプリID
        string appId = "ca-app-pub-5826884310810097~9739620388";

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestBanner();
        */
    }
    private void RequestBanner()
    {
        /*
        // 広告ユニットID これはテスト用
        string adUnitId = "ca-app-pub-5826884310810097/9300636685";

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
        */
    }

    public void BannerDestroy()
    {/*
        bannerView.Destroy();
        bannerView.Destroy();
        */
    }


    // Update is called once per frame
    void Update()
    {

    }
}