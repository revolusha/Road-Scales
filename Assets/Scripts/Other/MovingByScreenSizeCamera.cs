using UnityEngine;

[RequireComponent(typeof(Camera))]

public class MovingByScreenSizeCamera : MonoBehaviour
{
    [SerializeField] private Vector3 _mobileLocalPosition;
    [SerializeField] private Quaternion _mobileLocalRotation;
    [SerializeField] private float _mobileFOV;
    [SerializeField] private Vector3 _PCLocalPosition;
    [SerializeField] private Quaternion _PCLocalRotation;
    [SerializeField] private float _PCFOV;

    private Camera _camera;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        SetCameraSettings();
        UiScaleResizer.OnScreenSizeChanged += SetCameraSettings;
    }

    private void OnDisable()
    {
        UiScaleResizer.OnScreenSizeChanged -= SetCameraSettings;
    }

    private void SetCameraSettings()
    {
        if (Screen.height > Screen.width)
            SetMobileCamera();
        else
            SetPCCamera();
    }

    private void SetPCCamera()
    {
        transform.localPosition = _PCLocalPosition;
        transform.localRotation = _PCLocalRotation;
        _camera.fieldOfView = _PCFOV;
    }

    private void SetMobileCamera()
    {
        transform.localPosition = _mobileLocalPosition;
        transform.localRotation = _mobileLocalRotation;
        _camera.fieldOfView = _mobileFOV;
    }
}