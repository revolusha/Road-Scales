using UnityEngine;

public class AudioMuter : MonoBehaviour
{
    [SerializeField] private GameObject _muteSoundButton;
    [SerializeField] private GameObject _unmuteSoundButton;
    [SerializeField] private GameObject _muteMusicButton;
    [SerializeField] private GameObject _unmuteMusicButton;

    private float _lastSoundVolume = .7f;
    private float _lastMusicVolume = .7f;

    private void Start()
    {
        if (Game.SoundPlayer.Volume == 0)
        {
            MuteSound();
            _lastSoundVolume = .7f;
        }

        if (Game.MusicPlayer.Volume == 0)
        {
            MuteMusic();
            _lastMusicVolume = .7f;
        }
    }

    public void MuteMusic()
    {
        _lastMusicVolume = Game.MusicPlayer.Volume;
        Game.MusicPlayer.SetVolume(0);
        _muteMusicButton.SetActive(false);
        _unmuteMusicButton.SetActive(true);
    }

    public void UnmuteMusic()
    {
        Game.MusicPlayer.SetVolume(_lastMusicVolume);
        _unmuteMusicButton.SetActive(false);
        _muteMusicButton.SetActive(true);
    }

    public void MuteSound()
    {
        _lastSoundVolume = Game.SoundPlayer.Volume;
        Game.SoundPlayer.SetVolume(0);
        _muteSoundButton.SetActive(false);
        _unmuteSoundButton.SetActive(true);
    }

    public void UnmuteSound()
    {
        Game.SoundPlayer.SetVolume(_lastSoundVolume);
        _unmuteSoundButton.SetActive(false);
        _muteSoundButton.SetActive(true);
    }
}