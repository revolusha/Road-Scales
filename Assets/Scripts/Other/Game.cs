using UnityEngine;

public class Game : MonoBehaviour
{
    private Money _money;
    private MusicPlayer _musicPlayer;
    private SoundPlayer _soundPlayer;

    public static Game Instance { get; private set; }
    public static Money Money => Instance._money;
    public static MusicPlayer MusicPlayer => Instance._musicPlayer;
    public static SoundPlayer SoundPlayer => Instance._soundPlayer;
    public static bool IsReady { get; private set; }

    private void OnEnable()
    {
        _musicPlayer = GetComponentInChildren<MusicPlayer>();
        _soundPlayer = GetComponentInChildren<SoundPlayer>();

        if (Instance == null)
            Instance = this;

        _money = new Money();
        IsReady = true;
    }
}
