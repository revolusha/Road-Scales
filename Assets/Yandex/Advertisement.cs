using Agava.YandexGames;
using System;
using UnityEngine;

public class Advertisement : MonoBehaviour
{
    private const float MinimalDelayToShowAd = 120;
    private const float IntervalBetweenRewardAd = 240;

    private float _lastRewardTimeFromStartUp;

    private bool _isAllowedShowingAd = false;
    private bool _isReadyToShowRewardAd = false;

    public static Action OnRewardAdClosedSuccessful;
    public static Action OnRewardAdClosedFailed;

    public bool IsAllowedShowingAd
    {
        get
        {
            if (_isAllowedShowingAd)
                return true;

            _isAllowedShowingAd = Time.realtimeSinceStartup > MinimalDelayToShowAd;
            return _isAllowedShowingAd;
        }
    }

    public bool IsReadyToShowRewardAd
    {
        get
        {
            _isReadyToShowRewardAd = Time.realtimeSinceStartup - _lastRewardTimeFromStartUp > IntervalBetweenRewardAd;

            return _isReadyToShowRewardAd;
        }
    }

    private void OnEnable()
    {
        _lastRewardTimeFromStartUp = 0;
        OnRewardAdClosedSuccessful += ResetRewardTimer;
    }

    private void OnDisable()
    {
        OnRewardAdClosedSuccessful -= ResetRewardTimer;
    }

    public void TryShowInterstitialAd()
    {
        if (Game.Advertisement.IsAllowedShowingAd)
            SdkAndJavascriptHandler.CheckSdkConnection(ShowInterstitialAd, LevelReloader.ReloadBaseLevel);
        else
            LevelReloader.ReloadBaseLevel();
    }

    public void TryShowRewardAd()
    {
        const float EndShowDelay = 31;

        DelayRewardAd(EndShowDelay);

        if (Game.Advertisement.IsAllowedShowingAd)
            SdkAndJavascriptHandler.CheckSdkConnection(ShowRewardAd);
        else
            OnRewardAdClosedFailed?.Invoke();
    }

    public void DelayRewardAd(float seconds)
    {
        _lastRewardTimeFromStartUp = Time.realtimeSinceStartup - IntervalBetweenRewardAd + seconds;
    }

    public void ResetRewardTimer()
    {
        _lastRewardTimeFromStartUp = Time.realtimeSinceStartup;
    }

    private void ShowInterstitialAd()
    {
        InterstitialAd.Show(
            onCloseCallback: LevelReloader.LoadNextLevelAfterAd,
            onErrorCallback: LevelReloader.LoadNextLevelAfterAd,
            onOfflineCallback: LevelReloader.ReloadBaseLevel);
    }

    private void ShowRewardAd()
    {
        VideoAd.Show(
            onRewardedCallback: OnRewardAdClosedSuccessful,
            onCloseCallback: OnRewardAdClosedFailed);
    }
}