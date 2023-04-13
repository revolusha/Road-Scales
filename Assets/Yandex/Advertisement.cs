using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Advertisement : MonoBehaviour
{
    private const float MinimalIntervalToShowAd = 120;

    private bool _isAllowedShowingAd = false;

    private bool IsAllowedShowingAd
    {
        get 
        { 
            if (_isAllowedShowingAd)
                return true;

            _isAllowedShowingAd = CheckIfCanShowAd();
            return _isAllowedShowingAd;
        }
    }

    public void TryShowInterstitialAd()
    {
        if (Game.Advertisement.IsAllowedShowingAd)
            StartCoroutine(ShowAd());
        else
            LevelReloader.ReloadBaseLevel();
    }

    private bool CheckIfCanShowAd()
    {
        return Time.realtimeSinceStartup > MinimalIntervalToShowAd;
    }

    private IEnumerator ShowAd()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        InterstitialAd.Show(
            onCloseCallback: LevelReloader.LoadNextLevelAfterAd,
            onErrorCallback: LevelReloader.LoadNextLevelAfterAd, 
            onOfflineCallback: LevelReloader.ReloadBaseLevel);
    }
}