using Agava.YandexGames;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public const string PrefsKey = "GameSave";

    private static string _jsonDataString;

    public static void OnSaveEvent()
    {
        _jsonDataString = CreateSaveDataString();

        SaveLocal();
        SdkAndJavascriptHandler.CheckSdkConnection(Save);
    }

    public static void ClearSavedData()
    {
        const string EmptyJsonDataString = "{}";

        PlayerPrefs.DeleteKey(PrefsKey);
        SdkAndJavascriptHandler.CheckSdkConnection(() =>
        {
            PlayerAccount.SetPlayerData(
                EmptyJsonDataString, 
                onSuccessCallback: SdkAndJavascriptHandler.ReloadBrowserPage);
        });
    }

    public static void Save()
    {
        PlayerAccount.SetPlayerData(_jsonDataString);
    }

    public static void SaveLocal()
    {
        PlayerPrefs.SetString(PrefsKey, _jsonDataString);
    }

    private static string CreateSaveDataString()
    {
        PlayerInfo playerInfo = new();
        return playerInfo.SaveToString();
    }

    public void StartSaving()
    {
        OnSaveEvent();
    }
}