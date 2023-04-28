using UnityEngine;

public class StartGameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameData;
    [SerializeField] private LevelRoadConfiguration[] _levelsPool;

    private static bool _isInitialized;
    private static bool _gameInitialized;

    private void OnEnable()
    {
        DontDestroyOnLoad(_gameData);
        Initialize();
    }

    private void Start()
    {
        Debug.Log("Start StartGameInitializer");
        Game.OnGotReady += SetGameReadyFlag;
        SdkAndJavascriptHandler.CheckSdkConnection(Loading.Load, Loading.FinishLoading);
    }

    private void OnDisable()
    {
        Game.OnGotReady -= SetGameReadyFlag;
    }

    public static void TryFinishInitialization()
    {
        if (SdkAndJavascriptHandler.IsLocalized || Loading.IsLoadingDone ||
            _isInitialized || _gameInitialized)
            LevelReloader.ReloadBaseLevel();
    }

    private void SetGameReadyFlag()
    {
        _gameInitialized = true;
        TryFinishInitialization();
    }

    private void Initialize()
    {
        Debug.Log("Initialize");
        SdkAndJavascriptHandler.SetLanguage();
        Game.LevelHandler.SetLevelsPool(_levelsPool);
        _isInitialized = true;
        TryFinishInitialization();
    }
}