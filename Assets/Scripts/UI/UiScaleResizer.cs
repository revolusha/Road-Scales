using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]

public class UiScaleResizer : MonoBehaviour
{
    [SerializeField] private float _defaultHeight = 800;
    [SerializeField] private float _defaultWidth = 1200;
    [SerializeField] private float _checkResolutionTimeInterval = 2f;

    private Vector2 _resolution;
    private CanvasScaler _canvasScaler;
    private Coroutine _coroutine;

    private void Awake()
    {
        _canvasScaler = GetComponent<CanvasScaler>();
        _resolution = new Vector2(Screen.width, Screen.height);
        ResizeUI();
        RestartCoroutine();
    }

    private void ResizeUI()
    {
        float heightFactor = _defaultHeight / Screen.height;
        float widthFactor = _defaultWidth / Screen.width;

        if (heightFactor >= widthFactor)
            _canvasScaler.scaleFactor = 1 / heightFactor;
        else
            _canvasScaler.scaleFactor = 1 / widthFactor;

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
            MovingByScreenSizeCamera.SetCameraSettings();
        }

        yield return new WaitForSeconds(_checkResolutionTimeInterval);

        RestartCoroutine();
    }
}