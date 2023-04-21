using UnityEngine;

public class AudioMuterButtons : MonoBehaviour
{
    [SerializeField] private GameObject _muteSoundButton;
    [SerializeField] private GameObject _unmuteSoundButton;
    [SerializeField] private GameObject _muteMusicButton;
    [SerializeField] private GameObject _unmuteMusicButton;

    private void OnEnable()
    {
        AudioMuter.OnVolumeChanged += UpdateButtonVisibility;
    }

    private void Start()
    {
        UpdateButtonVisibility();
    }

    private void OnDisable()
    {
        AudioMuter.OnVolumeChanged -= UpdateButtonVisibility;
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

    private void UpdateButtonVisibility()
    {
        UpdateMusicButtonVisibility();
        UpdateSoundButtonVisibility();
    }

    private void UpdateSoundButtonVisibility()
    {
        _muteSoundButton.SetActive(AudioMuter.IsSoundMuted == false);
        _unmuteSoundButton.SetActive(AudioMuter.IsSoundMuted);
    }

    private void UpdateMusicButtonVisibility()
    {
        _muteMusicButton.SetActive(AudioMuter.IsMusicMuted == false);
        _unmuteMusicButton.SetActive(AudioMuter.IsMusicMuted);
    }
}