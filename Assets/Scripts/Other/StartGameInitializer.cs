using System.Collections;
using UnityEngine;

public class StartGameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameData;
    [SerializeField] private LevelRoadConfiguration[] _levelsPool;

    private static bool _isInitialized;

    private void OnEnable()
    {
        DontDestroyOnLoad(_gameData);
        StartCoroutine(Initialize());
    }

    public static void TryFinishInitialization()
    {
        if (SdkAndJavascriptHandler.IsLocalized && Loading.IsLoadingDone && _isInitialized)
            LevelReloader.ReloadBaseLevel();
    }

    private IEnumerator Initialize()
    {
        SdkAndJavascriptHandler.SetLanguage();
        Game.LevelHandler.SetLevelsPool(_levelsPool);
        _isInitialized = true;

        yield return Game.Initialize();
        yield return SkinHandler.Initialize();
        yield return SkinLoader.Initialize();

        SdkAndJavascriptHandler.CheckSdkConnection(Loading.Load, Loading.LoadLocalPlayerData);
    }
}