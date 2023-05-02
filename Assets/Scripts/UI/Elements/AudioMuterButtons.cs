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
        UpdateMusicButtonVisibility();
    }

    public void UnmuteMusic()
    {
        AudioMuter.UnmuteMusic();
        UpdateMusicButtonVisibility();
    }

    public void MuteSound()
    {
        AudioMuter.MuteSound();
        UpdateSoundButtonVisibility();
    }

    public void UnmuteSound()
    {
        AudioMuter.UnmuteSound();
        UpdateSoundButtonVisibility();
    }

    private static void UpdateSoundButtonVisibility()
    {
        _instance._muteSoundButton.SetActive(AudioMuter.IsSoundMuted == false);
        _instance._unmuteSoundButton.SetActive(AudioMuter.IsSoundMuted);
    }

    private static void UpdateMusicButtonVisibility()
    {
        _instance._muteMusicButton.SetActive(AudioMuter.IsMusicMuted == false);
        _instance._unmuteMusicButton.SetActive(AudioMuter.IsMusicMuted);
    }
}