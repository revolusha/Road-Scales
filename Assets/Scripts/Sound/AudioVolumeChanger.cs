using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private const float volumeZeroThreshold = 0.05f;

    private void OnEnable()
    {
        UpdateSlidersValue();
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
        AudioMuter.OnVolumeChanged += UpdateSlidersValue;
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        _soundSlider.onValueChanged.RemoveListener(ChangeSoundVolume);
        AudioMuter.OnVolumeChanged -= UpdateSlidersValue;
    }

    private void ChangeMusicVolume(float value)
    {
        if (value < volumeZeroThreshold)
            AudioMuter.MuteMusic();
        else
            ChangeAudioVolume(Game.MusicPlayer, value);
    }

    private void ChangeSoundVolume(float value)
    {
        if (value < volumeZeroThreshold)
            AudioMuter.MuteSound();
        else
            ChangeAudioVolume(Game.SoundPlayer, value);
    }

    private void ChangeAudioVolume(AudioPlayer player, float value)
    {
        player.SetVolume(value);
        AudioMuter.OnVolumeChanged?.Invoke();
    }

    private void UpdateSlidersValue()
    {
        _musicSlider.value = Game.MusicPlayer.Volume;
        _soundSlider.value = Game.SoundPlayer.Volume;
    }
}