using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;
using System;

public class GoogleAdmobInterstitial : MonoBehaviour
{/*
    // �ϐ�
    private InterstitialAd interstitial;
    private bool showing;

    void Start()
    {
        showing = false;
        // �L���ǂݍ���
        RequestInterstitial();
    }

    void Update()
    {
        // �L���\��
        if (interstitial.IsLoaded() && !showing)
        {
            interstitial.Show();
            showing = true;
        }
    }

    // �L���ǂݍ��ݏ���
    private void RequestInterstitial()
    {
        // �������[�X���Ɏ�����ID�ɕύX����
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

    // �V�[���J�ڏ���
    private void LoadNextScene()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // �I�u�W�F�N�g�̔j��
        interstitial.Destroy();
    }

    // ---�ȉ��A�C�x���g�n���h���[

    // �L���̓ǂݍ��݊�����
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
    }

    // �L���̓ǂݍ��ݎ��s��
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // ���̃V�[���ɑJ��
        LoadNextScene();
    }

    // �L�����f�o�C�X�̉�ʂ����ς��ɕ\�����ꂽ�Ƃ�
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
    }

    // �L��������Ƃ�
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        // ���̃V�[���ɑJ��
        LoadNextScene();
    }

    // �ʂ̃A�v���iGoogle Play �X�g�A�Ȃǁj���N��������
    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
    }
    */
}