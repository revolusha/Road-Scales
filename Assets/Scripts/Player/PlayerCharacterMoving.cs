using UnityEngine;

public class PlayerCharacterMoving : MonoBehaviour
{
    private const float TouchToStrafeValueFactor = 3;
    private const float MaxMoveLimit = 1;

    private float _startXLocalPosition;
    private float _targetXLocalPosition;

    private void OnEnable()
    {
        _startXLocalPosition = 0;
        _targetXLocalPosition = 0;
        PlayerTouchMovementInput.OnFingerMoved += AffectToTargetPosition;
        PlayerTouchMovementInput.OnFingerLost += ResetStartAffectValue;
        PlayerTouchMovementInput.OnFingerTouched += SetStartAffectValue;
        PlayerMouseMovementInput.OnMouseDragged += AffectToTargetPosition;
        PlayerMouseMovementInput.OnMouseReleased += ResetStartAffectValue;
        PlayerMouseMovementInput.OnMouseClicked += SetStartAffectValue;
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
        PlayerMouseMovementInput.OnMouseDragged -= AffectToTargetPosition;
        PlayerMouseMovementInput.OnMouseReleased -= ResetStartAffectValue;
        PlayerMouseMovementInput.OnMouseClicked -= SetStartAffectValue;
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

        float xLocalPosition = gameObject.transform.localPosition.x;

        gameObject.transform.localPosition = new Vector3(
            Mathf.Lerp(xLocalPosition, _targetXLocalPosition, Time.deltaTime * Speed), 0, 0 );
    }
}
