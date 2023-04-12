using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{
    private const string SceneName = "LevelBase";

    public static void ReloadBaseLevel()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        Loading.OnLoadingFinished -= ReloadBaseLevel;
    }

    public static void LoadDefaultLevel()
    {
        Game.LevelHandler.SwitchToLevel(0);
        ReloadBaseLevel();
        Loading.OnLoadingFinished -= LoadDefaultLevel;
    }

    public void SwitchToNextLevel()
    {
        Game.LevelHandler.SwitchToNextLevel();
    }

    public void SwitchToLevel(int index)
    {
        Game.LevelHandler.SwitchToLevel(index);
        ReloadBaseLevel();
    }

    public static void LoadNextLevel()
    {
        Game.LevelHandler.SwitchToNextLevel();

        if (Advertisement.CheckIfCanShowAd())
            ShowInterstitialAd();

        ReloadBaseLevel();
    }

    private static void LoadNextLevelAfterAd<T>(T _)
    {
        ReloadBaseLevel();
    }

    private static void ShowInterstitialAd()
    {
        Agava.YandexGames.InterstitialAd.Show(onCloseCallback: LoadNextLevelAfterAd, 
            onErrorCallback: LoadNextLevelAfterAd, onOfflineCallback: ReloadBaseLevel);
    }
}