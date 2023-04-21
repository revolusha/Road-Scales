using System;
using UnityEngine;

public class PlayerKeyboardMovementInput : MonoBehaviour
{
    [SerializeField] private float _strafeFactor;

    public const float StrafeLimit = 300;

    private const string Horizontal = nameof(Horizontal);
    private const string KeyLeft = "left";
    private const string KeyRight = "right";
    private const string KeyA = "a";
    private const string KeyD = "d";

    private float _strafeAmount = 0;

    public static Action<float> OnButtonHold;
    public static Action OnButtonReleased;
    public static Action OnButtonDown;

    private void Update()
    {
        if (CheckHorizontalButtonsUp())
            HandleButtonRelease();
        else if (CheckHorizontalButtonsDown())
            HandleButtonDown();
        else if (CheckHorizontalButtons())
        {
            HandleButtonHold();
            MakeMove();
        }
    }

    private void HandleButtonHold()
    {
        _strafeAmount -= Input.GetAxis(Horizontal) * _strafeFactor;
        _strafeAmount = Mathf.Clamp(_strafeAmount, -StrafeLimit, StrafeLimit);
    }

    private void HandleButtonRelease()
    {
        _strafeAmount = 0;
        OnButtonReleased?.Invoke();
    }

    private void HandleButtonDown()
    {
        _strafeAmount = 0;
        OnButtonDown?.Invoke();
    }

    private void MakeMove()
    {
        OnButtonHold?.Invoke(_strafeAmount);
    }

    private bool CheckHorizontalButtons()
    {
        return Input.GetKey(KeyA) || Input.GetKey(KeyD) || 
            Input.GetKey(KeyLeft) || Input.GetKey(KeyRight);
    }

    private bool CheckHorizontalButtonsUp()
    {
        return Input.GetKeyUp(KeyA) || Input.GetKeyUp(KeyD) ||
            Input.GetKeyUp(KeyLeft) || Input.GetKeyUp(KeyRight);
    }

    private bool CheckHorizontalButtonsDown()
    {
        return Input.GetKeyDown(KeyA) || Input.GetKeyDown(KeyD) ||
            Input.GetKeyDown(KeyLeft) || Input.GetKeyDown(KeyRight);
    }
}