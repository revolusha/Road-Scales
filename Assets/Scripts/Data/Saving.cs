using Agava.YandexGames;
using UnityEngine;

public class Saving : MonoBehaviour
{
    public static void OnSaveEvent()
    {
        SdkAndJavascriptHandler.CheckSdkConnection(Save);
    }

    public static void ClearSavedData()
    {
        const string EmptyJsonDataString = "{}";

        SdkAndJavascriptHandler.CheckSdkConnection(() =>
        {
            PlayerAccount.SetPlayerData(
                EmptyJsonDataString, 
                onSuccessCallback: SdkAndJavascriptHandler.ReloadBrowserPage);
        });
    }

    public static void Save()
    {
        PlayerInfo playerInfo = new();
        string jsonDataString = playerInfo.SaveToString();

        PlayerAccount.SetPlayerData(jsonDataString);
    }

    public void StartSaving()
    {
        OnSaveEvent();
    }
}