using Agava.YandexGames;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class SdkAndJavascriptHandler : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void ReloadPage();

    private static bool _isLocalized;

    private static SdkAndJavascriptHandler _instance;

    public static Action OnAuthorizedAndPersonalProfileDataGot;

    public static bool IsLocalized => _isLocalized;

    private void OnEnable()
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
        CheckSdkConnection(InitializeLocalization, InitializeSystemLocalization);
    }

    public static void TryAuthorize()
    {
        CheckSdkConnection(CheckAuthorization);
    }
    public static void CheckSdkConnection(Action onlineMethod, Action offlineMethod = null)
    {
        _instance.StartCoroutine(CheckConnectionJob(onlineMethod, offlineMethod));
    }

    private static void CheckPersonalProfileDataPermission()
    {
        if (PlayerAccount.HasPersonalProfileDataPermission == false)
            PlayerAccount.RequestPersonalProfileDataPermission(OnAuthorizedAndPersonalProfileDataGot);
        else
            OnAuthorizedAndPersonalProfileDataGot?.Invoke();
    }

    private static void CheckAuthorization()
    {
        if (PlayerAccount.IsAuthorized)
            CheckPersonalProfileDataPermission();
        else
            PlayerAccount.Authorize(CheckPersonalProfileDataPermission);
    }

    private static void InitializeLocalization()
    {
        string locale = YandexGamesSdk.Environment.i18n.lang;

        switch (locale)
        {
            case "ru":
                Localization.ChangeLocalization(Localization.LanguageType.Russian);
                break;

            case "en":
                Localization.ChangeLocalization(Localization.LanguageType.English);
                break;

            case "tr":
                Localization.ChangeLocalization(Localization.LanguageType.Turkish);
                break;

            default:
                Localization.ChangeLocalization(Localization.LanguageType.English);
                break;
        }

        FinishLocalization();
    }

    private static void InitializeSystemLocalization()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Russian:
                Localization.ChangeLocalization(Localization.LanguageType.Russian);
                break;

            case SystemLanguage.English:
                Localization.ChangeLocalization(Localization.LanguageType.English);
                break;

            case SystemLanguage.Turkish:
                Localization.ChangeLocalization(Localization.LanguageType.Turkish);
                break;

            default:
                Localization.ChangeLocalization(Localization.LanguageType.English);
                break;
        }

        FinishLocalization();
    }

    private static void FinishLocalization()
    {
        _isLocalized = true;
        StartGameInitializer.TryFinishInitialization();
    }

    private static IEnumerator CheckConnectionJob(Action onlineMethod, Action offlineMethod = null)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (offlineMethod != null)
            offlineMethod?.Invoke();

        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        onlineMethod?.Invoke();
    }
}