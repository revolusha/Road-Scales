using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class MusicPlayer : AudioPlayer
{
    [SerializeField] private AudioClip _loopedBGMusic;
    [SerializeField] private AudioClip _winningSound;

    private bool _isPlayingBGM = false;

    public bool IsPlayingBGM => _isPlayingBGM;

    private new void OnEnable()
    {
        base.OnEnable();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (_isPlayingBGM)
            return;

        _audio.Stop();
        _audio.clip = _loopedBGMusic;
        _audio.Play();
        _audio.loop = true;
        _isPlayingBGM = true;
    }

    public void PlayWinSound()
    {
        _isPlayingBGM = false;
        _audio.Stop();
        _audio.clip = _winningSound;
        _audio.Play();
        _audio.loop = false;
    }
}