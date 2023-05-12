using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private const float StartVolume = .6f;

    private float _lastVolume = .6f;

    protected AudioSource _audio;

    public float Volume => _audio.volume;
    public AudioSource AudioSource => _audio;
    public bool IsVolumesZero => _lastVolume == 0 && Volume == 0;

    protected void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = StartVolume;
    }

    public void SetVolume(float value, bool isFocused = true)
    {
        if (isFocused)
            _lastVolume = _audio.volume;

        _audio.volume = value;
    }

    public void SetLastVolume()
    {
        _audio.volume = _lastVolume;
    }

    public void SetDefaultVolume()
    {
        SetVolume(StartVolume);
    }

    public void Pause()
    {
        SetVolume(0);
    }

    public void Unpause()
    {
        SetLastVolume();
    }
}