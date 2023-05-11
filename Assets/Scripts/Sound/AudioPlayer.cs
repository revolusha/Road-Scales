using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private const float StartVolume = .7f;

    private float _lastVolume = .7f;

    protected AudioSource _audio;

    public float Volume => _audio.volume;
    public AudioSource AudioSource => _audio;

    protected void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = StartVolume;
    }

    public void SetVolume(float value)
    {
        _lastVolume = _audio.volume;
        _audio.volume = value;
    }

    public void SetLastVolume()
    {
        if (_lastVolume == 0)
            _lastVolume = StartVolume;

        _audio.volume = _lastVolume;
    }
}