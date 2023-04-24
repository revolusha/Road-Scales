using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]

public class UiScaleResizer : MonoBehaviour
{
    [SerializeField] private float _defaultHeight = 1000;
    [SerializeField] private float _defaultWidth = 1000;
    [SerializeField] private float _resolutionWideFactorThreshold = 2.1f;
    [SerializeField] private float _checkResolutionTimeInterval = 2f;

    private Vector2 _resolution;
    private CanvasScaler _canvasScaler;
    private Coroutine _coroutine;

    public static Action OnScreenSizeChanged;

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        _resolution = new Vector2(Screen.width, Screen.height);
        ResizeUI();
        RestartCoroutine();
    }

    private void Start()
    {
        OnScreenSizeChanged?.Invoke();
    }

    private void ResizeUI()
    {
        float heightFactor = Screen.height / _defaultHeight;
        float widthFactor = Screen.width / _defaultWidth;

        if (Screen.width > Screen.height)
            _canvasScaler.scaleFactor = heightFactor;
        else
            _canvasScaler.scaleFactor = widthFactor;

        _resolution.x = Screen.width;
        _resolution.y = Screen.height;
    }

    private void RestartCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(CheckScreenResolution());
    }

    private IEnumerator CheckScreenResolution()
    {
        if (_resolution.x != Screen.width || _resolution.y != Screen.height)
        {
            ResizeUI();
            OnScreenSizeChanged?.Invoke();
        }

        yield return new WaitForSeconds(_checkResolutionTimeInterval);

        RestartCoroutine();
    }
}