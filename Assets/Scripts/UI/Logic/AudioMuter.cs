using System;
using UnityEngine;

public class AudioMuter : MonoBehaviour
{
    private static float _lastSoundVolume = .7f;
    private static float _lastMusicVolume = .7f;
    private static bool _isSoundMuted;
    private static bool _isMusicMuted;

    public static Action OnVolumeChanged;

    public static bool IsMusicMuted => _isMusicMuted;
    public static bool IsSoundMuted => _isSoundMuted;

    private void Start()
    {
        if (Game.SoundPlayer.Volume == 0)
        {
            MuteSound();
        }
        else
        {
            UnmuteSound();
            _lastSoundVolume = Game.SoundPlayer.Volume;
        }

        if (Game.MusicPlayer.Volume == 0)
        {
            MuteMusic();
        }
        else
        {
            UnmuteMusic();
            _lastMusicVolume = Game.MusicPlayer.Volume;
        }
    }

    public static void MuteMusic(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isMusicMuted = true;

        _lastMusicVolume = Game.MusicPlayer.Volume;
        Game.MusicPlayer.SetVolume(0);
        OnVolumeChanged?.Invoke();
    }

    public static void UnmuteMusic(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isMusicMuted = false;

        Game.MusicPlayer.SetVolume(_lastMusicVolume);
        OnVolumeChanged?.Invoke();
    }

    public static void MuteSound(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isSoundMuted = true;

        _lastSoundVolume = Game.SoundPlayer.Volume;
        Game.SoundPlayer.SetVolume(0);
        OnVolumeChanged?.Invoke();
    }

    public static void UnmuteSound(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isSoundMuted = false;

        Game.SoundPlayer.SetVolume(_lastSoundVolume);
        OnVolumeChanged?.Invoke();
    }
}