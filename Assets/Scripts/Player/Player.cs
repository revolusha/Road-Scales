using UnityEngine;

public class Player : MonoBehaviour
{
    private const float TouchToStrafeValueFactor = 6;
    private const float MaxMoveLimit = 2;

    private float _startXLocalPosition;
    private float _targetXLocalPosition;

    private void OnEnable()
    {
        _startXLocalPosition = 0;
        _targetXLocalPosition = 0;
        PlayerTouchMovementInput.OnFingerMoved += AffectToTargetPosition;
        PlayerTouchMovementInput.OnFingerLost += ResetStartAffectValue;
        PlayerTouchMovementInput.OnFingerTouched += SetStartAffectValue;
    }

    private void Update()
    {
        if (gameObject.transform.localPosition.x != _targetXLocalPosition)
            Strafe();
    }

    private void OnDisable()
    {
        PlayerTouchMovementInput.OnFingerMoved -= AffectToTargetPosition;
        PlayerTouchMovementInput.OnFingerLost -= ResetStartAffectValue;
        PlayerTouchMovementInput.OnFingerTouched -= SetStartAffectValue;
    }

    private void SetStartAffectValue()
    {
        _startXLocalPosition = _targetXLocalPosition;
    }

    private void ResetStartAffectValue()
    {
        _startXLocalPosition = 0;
    }

    private void AffectToTargetPosition(float amount)
    {
        float affectValue = amount / PlayerTouchMovementInput.StrafeLimit * TouchToStrafeValueFactor;

        _targetXLocalPosition = Mathf.Clamp(_startXLocalPosition + affectValue, -MaxMoveLimit, MaxMoveLimit); ;
    }

    private void Strafe()
    {
        const float Speed = 4;

        Vector3 xLocalPosition = gameObject.transform.localPosition;

        gameObject.transform.localPosition = new Vector3(
            Mathf.Lerp(xLocalPosition.x, _targetXLocalPosition, Time.deltaTime * Speed), 
            0, 
            0 );

        TESTDebuggingLabels.ShowMessage(1, "Strafe: " + _targetXLocalPosition.ToString());
    }
}
