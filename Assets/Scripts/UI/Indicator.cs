using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class Indicator : MonoBehaviour
{
    private Coroutine _blinking;
    private Image _image;
    private SoundPlayer _audio;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;
        _audio = Game.SoundPlayer;
    }

    public void ShowWarning()
    {
        gameObject.SetActive(true);
        RestartBlinking();
    }

    public void HideWarning()
    {
        gameObject.SetActive(false);
        StopBlinking();
    }

    private void StopBlinking()
    {
        if (_blinking != null)
            StopCoroutine(_blinking);

        _image.enabled = false;
    }

    private void RestartBlinking()
    {
        StopBlinking();
        _blinking = StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        const float TimeInterval = 0.2f;
        const int LoopCount = 20;

        for (int i = 0; i < LoopCount; i++)
        {
            _image.enabled = true;
            _audio.PlayWarningAlarmSound();
            yield return new WaitForSeconds(TimeInterval);
            _image.enabled = false;
            yield return new WaitForSeconds(TimeInterval);
        }

        RestartBlinking();
    }
}