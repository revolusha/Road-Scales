using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{
    private const string SceneName = "LevelBase";

    public static void ReloadLevel()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        Loading.OnLoadingFinished -= ReloadLevel;
    }

    public static void LoadDefaultLevel()
    {
        Game.LevelHandler.SwitchToLevel(0);
        ReloadLevel();
        Loading.OnLoadingFinished -= LoadDefaultLevel;
    }

    public static void SwitchToNextLevel()
    {
        Game.LevelHandler.SwitchToNextLevel();
    }

    public static void LoadNextLevel()
    {
        if (Game.LevelHandler.IsLastLevel)
            return;

        Game.LevelHandler.SwitchToNextLevel();
        ReloadLevel();
    }
}
