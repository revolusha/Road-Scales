using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class StartGameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject _gameDataTemplate;
    [SerializeField] private LevelRoadConfiguration[] _levelsPool;

    private void OnEnable()
    {
        StartCoroutine(Initialize());
    }

    private void Start()
    {
        Loading.OnLoadingFinished += LoadLevel;
        StartCoroutine(Loading.Load());
    }

    private void OnDisable()
    {
        Loading.OnLoadingFinished -= LoadLevel;
    }

    private void LoadLevel()
    {
        LevelReloader.ReloadBaseLevel();
    }

    private IEnumerator Initialize()
    {        
        GameObject gameData = Instantiate(_gameDataTemplate);

        DontDestroyOnLoad(gameData);
        Game.LevelHandler.SetLevelsPool(_levelsPool);

#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();
    }
}