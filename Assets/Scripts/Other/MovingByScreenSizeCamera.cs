using System.Collections;
using System.Collections.Generic;
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

    private static MovingByScreenSizeCamera _instance;

    private Camera _camera;

    public static MovingByScreenSizeCamera Instance => _instance;

    private void OnEnable()
    {
        _instance = this;
        _camera = GetComponent<Camera>();
        SetCameraSettings();
    }

    public static void SetCameraSettings()
    {
        if (Screen.height > Screen.width)
            Instance.SetMobileCamera();
        else
            Instance.SetPCCamera();
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
