using System;
using UnityEngine;

public class AudioMuter : MonoBehaviour
{
    private static bool _isSoundMuted;
    private static bool _isMusicMuted;

    public static Action OnVolumeChanged;

    public static bool IsMusicMuted => _isMusicMuted;

    public static bool IsSoundMuted => _isSoundMuted;

    private void Start()
    {
        if (Game.SoundPlayer.Volume == 0)
            MuteSound();
        else
            UnmuteSound();

        if (Game.MusicPlayer.Volume == 0)
            MuteMusic();
        else
            UnmuteMusic();

        AudioMuterButtons.UpdateButtonVisibility();
    }

    public static void MuteMusic(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isMusicMuted = true;

        Game.MusicPlayer.SetVolume(0);
        OnVolumeChanged?.Invoke();
    }

    public static void UnmuteMusic(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isMusicMuted = false;

        Game.MusicPlayer.SetLastVolume();
        OnVolumeChanged?.Invoke();
    }

    public static void MuteSound(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isSoundMuted = true;

        Game.SoundPlayer.SetVolume(0);
        OnVolumeChanged?.Invoke();
    }

    public static void UnmuteSound(bool isStayInFocus = true)
    {
        if (isStayInFocus)
            _isSoundMuted = false;

        Game.SoundPlayer.SetLastVolume();
        OnVolumeChanged?.Invoke();
    }
}