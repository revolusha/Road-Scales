using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundPlayer : AudioPlayer
{
    [SerializeField] private AudioClip _scalesBreakSound;
    [SerializeField] private AudioClip _dropSound;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _error;
    [SerializeField] private AudioClip _warningAlarm;

    private new void OnEnable()
    {
        base.OnEnable();
        _audio.loop = false;
    }

    public void PlayButtonSound()
    {
        _audio.PlayOneShot(_buttonClick);
    }

    public void PlayScalesBreakSound()
    {
        _audio.PlayOneShot(_scalesBreakSound);
    }

    public void PlayCargoDropSound()
    {
        _audio.PlayOneShot(_dropSound);
    }

    public void PlayErrorSound()
    {
        _audio.PlayOneShot(_error);
    }

    public void PlayWarningAlarmSound()
    {
        _audio.PlayOneShot(_warningAlarm);
    }
}
