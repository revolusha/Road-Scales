using System.Collections;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] private MeshRenderer _sign;

    private bool _isPlaying = false;
    private bool _canPlay = true;
    private Coroutine _blinking;
    private SoundPlayer _audio;

    public bool IsPlaying => _isPlaying;

    private void Start()
    {
        _audio = Game.SoundPlayer;
        Scales.OnScalesBroke += TurnOff;
    }

    private void OnDisable()
    {
        Scales.OnScalesBroke -= TurnOff;
    }

    public void StopBlinking()
    {
        if (_blinking != null)
            StopCoroutine(_blinking);

        _isPlaying = false;
        _sign.enabled = true;
    }

    public void RestartBlinking()
    {
        if (_canPlay == false)
            return;

        StopBlinking();
        _isPlaying = true;
        _blinking = StartCoroutine(Blink());
    }

    private void TurnOff()
    {
        _canPlay = false;
        StopBlinking();
    }

    private IEnumerator Blink()
    {
        const float TimeInterval = 0.2f;
        const int LoopCount = 20;

        for (int i = 0; i < LoopCount; i++)
        {
            _sign.enabled = true;
            _audio.PlayWarningAlarmSound();
            yield return new WaitForSeconds(TimeInterval);
            _sign.enabled = false;
            yield return new WaitForSeconds(TimeInterval);
        }

        RestartBlinking();
    }
}