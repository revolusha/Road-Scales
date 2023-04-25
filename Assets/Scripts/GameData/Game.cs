using UnityEngine;

public class Game : MonoBehaviour
{
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
    public static bool IsReady { get; private set; }

    private void OnEnable()
    {
        _musicPlayer = GetComponentInChildren<MusicPlayer>();
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
        _skinHandler = GetComponentInChildren<SkinHandler>();
        _advertisement = GetComponentInChildren<Advertisement>();

        if (Instance == null)
            Instance = this;

        _money = new Money();
        _levelHandler = new LevelHandler();
        _levelHandler.OnLastLevelFinished += OnLastLevelFinishedEvent;
        TutorialComponentsHandler.OnTutorialFinished += OnTutorialFinishedEvent;
        IsReady = true;
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
        TutorialComponentsHandler.OnTutorialFinished -= OnTutorialFinishedEvent;
    }
}