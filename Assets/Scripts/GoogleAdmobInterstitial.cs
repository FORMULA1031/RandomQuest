using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;
using System;

public class GoogleAdmobInterstitial : MonoBehaviour
{/*
    // 変数
    private InterstitialAd interstitial;
    private bool showing;

    void Start()
    {
        showing = false;
        // 広告読み込み
        RequestInterstitial();
    }

    void Update()
    {
        // 広告表示
        if (interstitial.IsLoaded() && !showing)
        {
            interstitial.Show();
            showing = true;
        }
    }

    // 広告読み込み処理
    private void RequestInterstitial()
    {
        // ★リリース時に自分のIDに変更する
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-5826884310810097/8725921610";
#elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-5826884310810097/8725921610";
#else
                string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    // シーン遷移処理
    private void LoadNextScene()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // オブジェクトの破棄
        interstitial.Destroy();
    }

    // ---以下、イベントハンドラー

    // 広告の読み込み完了時
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }

    // 広告の読み込み失敗時
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // 次のシーンに遷移
        LoadNextScene();
    }

    // 広告がデバイスの画面いっぱいに表示されたとき
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
    }

    // 広告を閉じたとき
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // 次のシーンに遷移
        LoadNextScene();
    }

    // 別のアプリ（Google Play ストアなど）を起動した時
    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
    }
    */
}