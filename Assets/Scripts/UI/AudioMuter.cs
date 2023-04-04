using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMuter : MonoBehaviour
{
    [SerializeField] private GameObject _muteButton;
    [SerializeField] private GameObject _unmuteButton;

    private float _lastSoundVolume = .7f;
    private float _lastMusicVolume = .7f;

    private void Start()
    {
        if (Game.SoundPlayer.Volume == 0 && Game.MusicPlayer.Volume == 0)
        {
            Mute();
            _lastSoundVolume = .7f;
            _lastMusicVolume = .7f;
        }
    }

    public void Mute()
    {
        _lastSoundVolume = Game.SoundPlayer.Volume;
        _lastMusicVolume = Game.MusicPlayer.Volume;
        Game.SoundPlayer.SetVolume(0);
        Game.MusicPlayer.SetVolume(0);
        _muteButton.SetActive(false);
        _unmuteButton.SetActive(true);
    }

    public void Unmute()
    {
        Game.SoundPlayer.SetVolume(_lastSoundVolume);
        Game.MusicPlayer.SetVolume(_lastMusicVolume);
        _unmuteButton.SetActive(false);
        _muteButton.SetActive(true);
    }
}
