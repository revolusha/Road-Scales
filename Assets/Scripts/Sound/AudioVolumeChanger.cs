using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeChanger : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    private void OnEnable()
    {
        _musicSlider.value = Game.MusicPlayer.Volume;
        _soundSlider.value = Game.SoundPlayer.Volume;
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _soundSlider.onValueChanged.AddListener(ChangeSoundVolume);
    }

    private void OnDisable()
    {
        _musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        _soundSlider.onValueChanged.RemoveListener(ChangeSoundVolume);
    }

    private void ChangeMusicVolume(float value)
    {
        Game.MusicPlayer.SetVolume(value);
    }

    private void ChangeSoundVolume(float value)
    {
        Game.SoundPlayer.SetVolume(value);
    }
}
