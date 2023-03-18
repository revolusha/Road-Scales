using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{
    private const string _levelScene = "LevelBase";

    public void ReloadLevel()
    {
        SceneManager.LoadScene(_levelScene, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        if (StaticInstances.IsLastLevel)
            return;

        StaticInstances.SwitchToNextLevel();
        ReloadLevel();
    }
}
