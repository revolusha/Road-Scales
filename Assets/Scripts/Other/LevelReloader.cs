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

    public static void LoadNextLevelAfterAd<T>(T _)
    {
        ReloadBaseLevel();
    }

    public static void LoadNextLevel()
    {
        Game.LevelHandler.SwitchToNextLevel();
        Game.Advertisement.TryShowInterstitialAd();
    }

    public static void SwitchToNextLevel()
    {
        Game.LevelHandler.SwitchToNextLevel();
    }

    public static void SwitchToLevel(int index)
    {
        Game.LevelHandler.SwitchToLevel(index);
        ReloadBaseLevel();
    }

    public static void ReloadLevelThroughAd()
    {
        Game.Advertisement.TryShowInterstitialAd();
    }
}