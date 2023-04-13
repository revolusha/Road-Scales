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

    public void LoadNextLevel()
    {
        Game.LevelHandler.SwitchToNextLevel();
        Game.Advertisement.TryShowInterstitialAd();
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

    public void ReloadLevelThroughAd()
    {
        Game.Advertisement.TryShowInterstitialAd();
    }
}