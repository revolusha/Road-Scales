using UnityEngine;

[RequireComponent(typeof(LevelReloader))]

public class StartGameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameDataTemplate;
    [SerializeField] private LevelRoadConfiguration[] _levelsPool;

    private void OnEnable()
    {
        Initialize();
    }

    private void Start()
    {
        GetComponent<LevelReloader>().ReloadLevel();
    }

    private void Initialize()
    {        
        GameObject gameData = Instantiate(_gameDataTemplate);

        DontDestroyOnLoad(gameData);
        StaticInstances.SetLevelsPool(_levelsPool);
    }

    //private void ResizeWindow()
    //{
    //    const float TargetResolutionHeightFactor = 16;
    //    const float TargetResolutionWidthFactor = 9;

    //    float oldResolutionFactor = Screen.width / Screen.height;
    //    int newWidth = (int)(Screen.width * oldResolutionFactor * TargetResolutionWidthFactor / TargetResolutionHeightFactor);

    //    Screen.SetResolution(newWidth, Screen.height, Screen.fullScreen);
    //}
}