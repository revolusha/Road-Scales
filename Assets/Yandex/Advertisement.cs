using Agava.YandexGames;
using System;
using System.Collections;
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
            StartCoroutine(ShowInterstitialAd());
        else
            LevelReloader.ReloadBaseLevel();
    }

    public void TryShowRewardAd()
    {
        const float EndShowDelay = 31;

        DelayRewardAd(EndShowDelay);

        if (Game.Advertisement.IsAllowedShowingAd)
            StartCoroutine(ShowRewardAd());
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

    private IEnumerator ShowInterstitialAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        LevelReloader.ReloadBaseLevel();
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        InterstitialAd.Show(
            onCloseCallback: LevelReloader.LoadNextLevelAfterAd,
            onErrorCallback: LevelReloader.LoadNextLevelAfterAd,
            onOfflineCallback: LevelReloader.ReloadBaseLevel);
    }

    private IEnumerator ShowRewardAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        VideoAd.Show(
            onRewardedCallback: OnRewardAdClosedSuccessful,
            onCloseCallback: OnRewardAdClosedFailed);
    }
}