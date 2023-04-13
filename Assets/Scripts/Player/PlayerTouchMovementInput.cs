using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovementInput : MonoBehaviour
{
    public const float StrafeLimit = 300;

    [SerializeField] private float _deadZone = .1f;

    private float _strafeAmount = 0;
    private Vector2 _touchOrigin;
    private Finger _finger;

    public static Action<float> OnFingerMoved;
    public static Action OnFingerLost;
    public static Action OnFingerTouched;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += OnFingerDown;
        ETouch.Touch.onFingerUp += OnFingerUp;
        ETouch.Touch.onFingerMove += OnFingerMove;
    }

    private void Update()
    {
        if (_finger == null)
            return;

        MakeMove();
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= OnFingerDown;
        ETouch.Touch.onFingerUp -= OnFingerUp;
        ETouch.Touch.onFingerMove -= OnFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void MakeMove()
    {
        OnFingerMoved?.Invoke(_strafeAmount);
    }

    private void OnFingerDown(Finger touchedFinger)
    {
        Debug.Log("Down");
        if (_finger == null)
        {
            _finger = touchedFinger;
            _strafeAmount = 0;
            _touchOrigin = touchedFinger.screenPosition;
        }

        OnFingerTouched?.Invoke();
    }

    private void OnFingerMove(Finger movedFinger)
    {
        Debug.Log("Move");
        if (movedFinger == _finger)
        {
            float fingerMoveAmount = _touchOrigin.x - _finger.screenPosition.x;

            _strafeAmount = Mathf.Clamp(fingerMoveAmount, -StrafeLimit, StrafeLimit);

            if (Mathf.Abs(_strafeAmount) < _deadZone)
                _strafeAmount = 0;
        }
    }

    private void OnFingerUp(Finger touchedFinger)
    {
        Debug.Log("Up");
        if (touchedFinger == _finger)
        {
            _finger = null;
            _strafeAmount = 0;
        }

        OnFingerLost?.Invoke();
    }
}