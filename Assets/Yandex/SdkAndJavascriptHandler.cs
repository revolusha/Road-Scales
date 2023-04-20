using Agava.YandexGames;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
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
        _instance.StartCoroutine(InitializeLocalization());
    }

    public static void TryAuthorize(Action onAuthorizedSuccessfulEvent = null)
    {
        if (PlayerAccount.IsAuthorized)
            onAuthorizedSuccessfulEvent?.Invoke();

        _instance.StartCoroutine(InitializeAuthorization(onAuthorizedSuccessfulEvent));
    }

    public static void FinishLocalization()
    {
        _isLocalized = true;
        Loading.TryCompleteLoading();
    }

    private static void Authorize(Action onAuthorizedSuccessfulEvent)
    {
        PlayerAccount.RequestPersonalProfileDataPermission();

        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize(onSuccessCallback: onAuthorizedSuccessfulEvent);
    }

    private static IEnumerator InitializeAuthorization(Action onAuthorizedSuccessfulEvent)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        Authorize(onAuthorizedSuccessfulEvent);
    }

    private static IEnumerator InitializeLocalization()
    {
        yield return LocalizationSettings.InitializationOperation;

#if !UNITY_WEBGL || UNITY_EDITOR
        FinishLocalization();
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        string locale = YandexGamesSdk.Environment.i18n.lang;

        for (int i = 0; i < _languageIndexes.Length; i++)
        {
            if (_languageIndexes[i] == locale)
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(locale);
        }

        FinishLocalization();
    }
}

public class WebEventSystem : EventSystem
{
    protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
}