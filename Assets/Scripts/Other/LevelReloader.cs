using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReloader : MonoBehaviour
{
    private const string _levelScene = "LevelBase";

    private Action OnAllLevelsWon; 

    public void ReloadLevel()
    {
        SceneManager.LoadScene(_levelScene, LoadSceneMode.Single);
    }

    public void LoadNextLevel()
    {
        if (StaticInstances.IsLastLevel)
        {
            OnAllLevelsWon?.Invoke();
            return;
        }

        StaticInstances.SwitchToNextLevel();
        ReloadLevel();
    }
}
