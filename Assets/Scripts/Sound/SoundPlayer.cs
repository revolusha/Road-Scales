using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SoundPlayer : AudioPlayer
{
    [SerializeField] private AudioClip _scalesBreakSound;
    [SerializeField] private AudioClip _dropSound;
    [SerializeField] private AudioClip _buttonClick;

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
}
