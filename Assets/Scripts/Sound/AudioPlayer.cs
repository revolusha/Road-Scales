using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private const float StartVolume = .7f;

    protected AudioSource _audio;

    protected bool _isReady = false;

    public bool IsReady => _isReady;
    public float Volume => _audio.volume;
    public AudioSource AudioSource => _audio;

    protected void OnEnable()
    {
        _audio = GetComponent<AudioSource>();
        _audio.volume = StartVolume;
        _isReady = true;
    }

    public void SetVolume(float value)
    {
        _audio.volume = value;
    }
}