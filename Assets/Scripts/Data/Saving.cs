using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Saving : MonoBehaviour
{
    private static Saving _instance;

    private void OnEnable()
    {
        _instance = this;
    }

    public static void OnSaveEvent()
    {
        _instance.StartCoroutine(Save());
    }

    public static void ClearSavedData()
    {
        const string EmptyJsonDataString = "{}";

        PlayerAccount.SetPlayerData(EmptyJsonDataString, onSuccessCallback: SdkAndJavascriptHandler.ReloadBrowserPage);
    }

    public void StartSaving()
    {
        OnSaveEvent();
    }

    public static IEnumerator Save()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        PlayerInfo playerInfo = new();
        string jsonDataString = playerInfo.SaveToString();

        PlayerAccount.SetPlayerData(jsonDataString);
    }
}