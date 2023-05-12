using System;
using UnityEngine;

public class AudioMuter : MonoBehaviour
{
    public static Action OnVolumeChanged;

    private void Start()
    {
        if (Game.SoundPlayer.Volume == 0)
            MuteSound();
        else
            UnmuteAudio(Game.SoundPlayer);

        if (Game.MusicPlayer.Volume == 0)
            MuteMusic();
        else
            UnmuteAudio(Game.MusicPlayer);

        AudioMuterButtons.UpdateButtonVisibility();
    }

    public static void Pause()
    {
        Game.MusicPlayer.Pause();
        Game.SoundPlayer.Pause();
    }

    public static void Unpause()
    {
        Game.MusicPlayer.Unpause();
        Game.SoundPlayer.Unpause();
    }

    public static void MuteMusic()
    {
        Game.MusicPlayer.SetVolume(0);
        OnVolumeChanged?.Invoke();
    }

    public static void MuteSound()
    {
        Game.SoundPlayer.SetVolume(0);
        OnVolumeChanged?.Invoke();
    }

    public static void UnmuteAudio(AudioPlayer player)
    {
        if (player.IsVolumesZero)
            player.SetDefaultVolume();
        else
            player.SetLastVolume();

        OnVolumeChanged?.Invoke();
    }
}