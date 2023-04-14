using System;
using UnityEngine;

public class PlayerTouchMovementInput : MonoBehaviour
{
    [SerializeField] private float _deadZone = .1f;

    public const float StrafeLimit = 300;

    private float _strafeAmount = 0;
    private Vector2 _touchOrigin;
    private int _currentFingerId;
    private bool _isTouched;

    public static Action<float> OnFingerMoved;
    public static Action OnFingerLost;
    public static Action OnFingerTouched;

    private void Update()
    {
        HandleInput();

        if (_isTouched)
            MakeMove();
    }

    private void HandleInput()
    {
        Debug.Log("true");
        if (Input.touches.Length < 1)
            return;

        if (_isTouched)
            OnFingerMove();
        else
            OnFingerDown();
    }

    private void MakeMove()
    {
        OnFingerMoved?.Invoke(_strafeAmount);
    }

    private void OnFingerDown()
    {
        Debug.Log("Down");
        Touch touch = Input.GetTouch(0);

        _currentFingerId = touch.fingerId;
        _strafeAmount = 0;
        _touchOrigin = touch.position;
        _isTouched = true;
        OnFingerTouched?.Invoke();
    }

    private void OnFingerMove()
    {
        Debug.Log("Move");
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId != _currentFingerId)
                continue;

            float fingerMoveAmount = _touchOrigin.x - touch.position.x;

            _strafeAmount = Mathf.Clamp(fingerMoveAmount, -StrafeLimit, StrafeLimit);

            if (Mathf.Abs(_strafeAmount) < _deadZone)
                _strafeAmount = 0;

            return;
        }

        OnFingerUp();
    }

    private void OnFingerUp()
    {
        Debug.Log("Up");
        _strafeAmount = 0;
        _isTouched = false;
        OnFingerLost?.Invoke();
    }
}