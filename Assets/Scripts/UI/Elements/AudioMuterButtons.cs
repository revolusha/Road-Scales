using UnityEngine;

public class AudioMuterButtons : MonoBehaviour
{
    [SerializeField] private GameObject _muteSoundButton;
    [SerializeField] private GameObject _unmuteSoundButton;
    [SerializeField] private GameObject _muteMusicButton;
    [SerializeField] private GameObject _unmuteMusicButton;

    private static AudioMuterButtons _instance;

    public static AudioMuterButtons Instance => _instance;

    private void OnEnable()
    {
        _instance = this;
        UpdateButtonVisibility();
        AudioMuter.OnVolumeChanged += UpdateButtonVisibility;
    }

    private void OnDisable()
    {
        AudioMuter.OnVolumeChanged -= UpdateButtonVisibility;
    }

    public static void UpdateButtonVisibility()
    {
        if (_instance == null)
            return;

        UpdateMusicButtonVisibility();
        UpdateSoundButtonVisibility();
    }

    public void MuteMusic()
    {
        AudioMuter.MuteMusic();
    }

    public void UnmuteMusic()
    {
        AudioMuter.UnmuteAudio(Game.MusicPlayer);
    }

    public void MuteSound()
    {
        AudioMuter.MuteSound();
    }

    public void UnmuteSound()
    {
        AudioMuter.UnmuteAudio(Game.SoundPlayer);
    }

    private static void UpdateSoundButtonVisibility()
    {
        bool isTurnedOn = CheckAudioLevel(Game.SoundPlayer);

        _instance._muteSoundButton.SetActive(isTurnedOn);
        _instance._unmuteSoundButton.SetActive(isTurnedOn == false);
    }

    private static void UpdateMusicButtonVisibility()
    {
        bool isTurnedOn = CheckAudioLevel(Game.MusicPlayer);

        _instance._muteMusicButton.SetActive(isTurnedOn);
        _instance._unmuteMusicButton.SetActive(isTurnedOn == false);
    }

    private static bool CheckAudioLevel(AudioPlayer player) 
    {
        if (player.Volume > 0)
            return true;
        else
            return false;
    }
}