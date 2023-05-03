using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Advertisement))]

public class Game : MonoBehaviour
{
    private static bool _isReady = false;

    private bool _isLastLevelFinished;
    private bool _isTutorialFinished;

    private Money _money;
    private LevelHandler _levelHandler;
    private SkinHandler _skinHandler;
    private MusicPlayer _musicPlayer;
    private SoundPlayer _soundPlayer;
    private Advertisement _advertisement;

    public static Game Instance { get; private set; }
    public static Money Money => Instance._money;
    public static MusicPlayer MusicPlayer => Instance._musicPlayer;
    public static SoundPlayer SoundPlayer => Instance._soundPlayer;
    public static SkinHandler SkinHandler => Instance._skinHandler;
    public static LevelHandler LevelHandler => Instance._levelHandler;
    public static Advertisement Advertisement => Instance._advertisement;
    public static bool IsLastLevelFinished => Instance._isLastLevelFinished;
    public static bool IsTutorialFinished => Instance._isTutorialFinished;

    private void Awake()
    {
        _musicPlayer = GetComponentInChildren<MusicPlayer>();
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
        _skinHandler = GetComponentInChildren<SkinHandler>();
        _advertisement = GetComponent<Advertisement>();

        if (Instance == null)
            Instance = this;

        _money = new Money();
        _levelHandler = new LevelHandler();
        _levelHandler.OnLastLevelFinished += OnLastLevelFinishedEvent;
        TutorialComponentsHandler.OnTutorialFinished += OnTutorialFinishedEvent;
        _isReady = true;
    }

    private void OnDisable()
    {
        _levelHandler.OnLastLevelFinished -= OnLastLevelFinishedEvent;
        TutorialComponentsHandler.OnTutorialFinished -= OnTutorialFinishedEvent;
    }

    public void SetFlags(bool isAllLevelsFinished, bool isTutorialFinished)
    {
        _isTutorialFinished = isTutorialFinished;
        _isLastLevelFinished = isAllLevelsFinished;

        if (isTutorialFinished == true)
            TutorialComponentsHandler.OnTutorialFinished -= OnTutorialFinishedEvent;
        if (isAllLevelsFinished == true)
            _levelHandler.OnLastLevelFinished -= OnLastLevelFinishedEvent;
    }

    private void OnLastLevelFinishedEvent()
    {
        _isLastLevelFinished = true;
        _levelHandler.OnLastLevelFinished -= OnLastLevelFinishedEvent;
    }

    private void OnTutorialFinishedEvent()
    {
        _isTutorialFinished = true;
        Saving.Save();
        TutorialComponentsHandler.OnTutorialFinished -= OnTutorialFinishedEvent;
    }

    public static IEnumerator Initialize()
    {
        while (_isReady == false) 
            yield return null;
    }
}