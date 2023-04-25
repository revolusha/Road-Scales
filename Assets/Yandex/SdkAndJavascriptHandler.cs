using Agava.YandexGames;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class SdkAndJavascriptHandler : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void ReloadPage();

    private static bool _isLocalized;

    private static SdkAndJavascriptHandler _instance;

    private readonly static string[] _languageIndexes = { "en", "ru", "tr" };

    public static bool IsLocalized => _isLocalized;

    private void Awake()
    {
        _isLocalized = false;
        YandexGamesSdk.CallbackLogging = true;
        _instance = this;
    }

    public static void ReloadBrowserPage()
    {
        ReloadPage(); 
    }

    public static void SetLanguage()
    {
        CheckSdkConnection(InitializeLocalization, FinishLocalization);
    }

    public static void TryAuthorize(Action onAuthorizedSuccessfulEvent = null)
    {
        bool isAuthorized = false;

        CheckSdkConnection(() => { isAuthorized = true; });

        if (isAuthorized)
            Authorize(onAuthorizedSuccessfulEvent);
    }

    public static void FinishLocalization()
    {
        _isLocalized = true;
        Loading.TryCompleteLoading();
    }

    private static void Authorize(Action onAuthorizedSuccessfulEvent)
    {
        if (PlayerAccount.IsAuthorized)
            onAuthorizedSuccessfulEvent?.Invoke();
        else
            PlayerAccount.Authorize(onSuccessCallback: onAuthorizedSuccessfulEvent);

        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    private static void InitializeLocalization()
    {
        _instance.StartCoroutine(InitializeLocalizationJob());
    }

    public static void CheckSdkConnection(Action onlineMethod, Action offlineMethod = null)
    {
        _instance.StartCoroutine(CheckConnectionJob(onlineMethod, offlineMethod));
    }

    private static IEnumerator CheckConnectionJob(Action onlineMethod, Action offlineMethod)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        offlineMethod?.Invoke();

        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        onlineMethod.Invoke();
    }

    private static IEnumerator InitializeLocalizationJob()
    {
        yield return LocalizationSettings.InitializationOperation;

        string locale = YandexGamesSdk.Environment.i18n.lang;

        for (int i = 0; i < _languageIndexes.Length; i++)
        {
            if (_languageIndexes[i] == locale)
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale);
        }

        FinishLocalization();
    }
}