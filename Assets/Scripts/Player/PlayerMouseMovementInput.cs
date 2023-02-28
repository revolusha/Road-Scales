using System;
using UnityEngine;

public class PlayerMouseMovementInput : MonoBehaviour
{
    public const float StrafeLimit = 300;

    private const string LeftMouseButton = "Fire1";

    [SerializeField] private float _deadZone = .1f;
    [SerializeField] private float _screenFactor = 720f;

    private float _strafeAmount = 0;
    private float _strafeScreenFactor;
    private Vector3 _mouseStartPosition;

    public static Action<float> OnMouseDragged;
    public static Action OnMouseReleased;
    public static Action OnMouseClicked;

    private void OnEnable()
    {
        _strafeScreenFactor = _screenFactor / Screen.width;
    }

    private void Update()
    {
        if (Input.GetButtonUp(LeftMouseButton))
            HandleLeftMouseButtonRelease();
        else if (Input.GetButtonDown(LeftMouseButton))
            HandleLeftMouseButtonClick();
        else if (Input.GetButton(LeftMouseButton))
        {
            HandleLeftMouseButton();
            MakeMove();
        } 
    }

    private void HandleLeftMouseButton()
    {
        float cursorMoveAmount = (_mouseStartPosition.x - Input.mousePosition.x) * _strafeScreenFactor;
        _strafeAmount = Mathf.Clamp(cursorMoveAmount, -StrafeLimit, StrafeLimit);

        if (Mathf.Abs(_strafeAmount) < _deadZone)
            _strafeAmount = 0;

        TESTUDATELABEL(0);
    }

    private void HandleLeftMouseButtonRelease()
    {
        _strafeAmount = 0;
        OnMouseReleased?.Invoke();
        TESTUDATELABEL(0);
    }

    private void HandleLeftMouseButtonClick()
    {
        _mouseStartPosition = Input.mousePosition;
        OnMouseClicked?.Invoke();
    }

    private void MakeMove()
    {
        OnMouseDragged?.Invoke(_strafeAmount);
    }

    private void TESTUDATELABEL(int code)
    {
        TESTDebuggingLabels.ShowMessage(code, "Touch: " + _strafeAmount.ToString());
    }
}
