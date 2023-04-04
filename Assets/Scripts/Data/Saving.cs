using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Saving : MonoBehaviour
{
    private static Saving _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void OnFinishSaveEvent()
    {
        _instance.StartCoroutine(Save());
    }

    public static IEnumerator Save()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        PlayerInfo playerInfo = new PlayerInfo();
        string jsonDataString = playerInfo.SaveToString();

        PlayerAccount.SetPlayerData(jsonDataString);
    }

    public static void ClearSavedData()
    {
        const string EmptyJsonDataString = "{}";

        PlayerAccount.SetPlayerData(EmptyJsonDataString, onSuccessCallback: JavaScriptHandler.ReloadBrowserPage);
    }
}
