using Agava.YandexGames;
using System.Runtime.InteropServices;
using UnityEngine;

public class JavaScriptHandler : MonoBehaviour
{
    [DllImport("__Internal")] private static extern void ReloadPage();

    public static void ReloadBrowserPage()
    {
        ReloadPage();
    }

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }
}