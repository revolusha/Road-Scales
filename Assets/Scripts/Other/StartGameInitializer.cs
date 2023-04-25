using UnityEngine;

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
        Loading.OnFullLoadingFinished += LoadLevel;
        SdkAndJavascriptHandler.CheckSdkConnection(Loading.Load, Loading.FinishLoading);
    }

    private void OnDisable()
    {
        Loading.OnFullLoadingFinished -= LoadLevel;
    }

    private void LoadLevel()
    {
        LevelReloader.ReloadBaseLevel();
    }

    private void Initialize()
    {        
        GameObject gameData = Instantiate(_gameDataTemplate);

        DontDestroyOnLoad(gameData);
        Game.LevelHandler.SetLevelsPool(_levelsPool);
        SdkAndJavascriptHandler.SetLanguage();
    }
}