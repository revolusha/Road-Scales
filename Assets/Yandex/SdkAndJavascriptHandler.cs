using Agava.YandexGames;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class SdkAndJavascriptHandler : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void ReloadPage();

    private static SdkAndJavascriptHandler _instance;

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
        _instance = this;
    }
    public static void ReloadBrowserPage()
    {
        ReloadPage();
    }

    public static void TryAuthorize(Action onAuthorizedSuccessfulEvent = null)
    {
        if (PlayerAccount.IsAuthorized)
            onAuthorizedSuccessfulEvent?.Invoke();

        _instance.StartCoroutine(InitializeAuthorization(onAuthorizedSuccessfulEvent));
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
}

public class WebEventSystem : EventSystem
{
    protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
}