using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private TESTMapLoader _TEST;

    private Money _money;
    private LevelHandler _levelHandler;
    private SkinHandler _skinHandler;
    private MusicPlayer _musicPlayer;
    private SoundPlayer _soundPlayer;

    public static bool ISTEST = false;

    public static Game Instance { get; private set; }
    public static Money Money => Instance._money;
    public static MusicPlayer MusicPlayer => Instance._musicPlayer;
    public static SoundPlayer SoundPlayer => Instance._soundPlayer;
    public static SkinHandler SkinHandler => Instance._skinHandler;
    public static LevelHandler LevelHandler => Instance._levelHandler;
    public static TESTMapLoader TEST => Instance._TEST;
    public static bool IsReady { get; private set; }

    private void OnEnable()
    {
        _musicPlayer = GetComponentInChildren<MusicPlayer>();
        _soundPlayer = GetComponentInChildren<SoundPlayer>();
        _skinHandler = GetComponentInChildren<SkinHandler>();

        if (Instance == null)
            Instance = this;

        _money = new Money();
        _levelHandler = new LevelHandler();
        IsReady = true;
    }
}
