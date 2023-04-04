using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoopRotation : MonoBehaviour
{
    private const float InputToRotationValueFactor = 1.5f;
    private const float MaxRotateLimit = .5f;

    private float _startYLocalRotation;
    private float _targetYLocalRotation;

    private void OnEnable()
    {
        _startYLocalRotation = 0;
        _targetYLocalRotation = 0;
        PlayerTouchMovementInput.OnFingerMoved += AffectRotation;
        PlayerTouchMovementInput.OnFingerLost += ResetStartAffectValue;
        PlayerTouchMovementInput.OnFingerTouched += SetStartAffectValue;
        PlayerMouseMovementInput.OnMouseDragged += AffectRotation;
        PlayerMouseMovementInput.OnMouseReleased += ResetStartAffectValue;
        PlayerMouseMovementInput.OnMouseClicked += SetStartAffectValue;
    }

    private void Update()
    {
        if (transform.localRotation.x != _targetYLocalRotation)
            Rotate();
    }

    private void OnDisable()
    {
        PlayerTouchMovementInput.OnFingerMoved -= AffectRotation;
        PlayerTouchMovementInput.OnFingerLost -= ResetStartAffectValue;
        PlayerTouchMovementInput.OnFingerTouched -= SetStartAffectValue;
        PlayerMouseMovementInput.OnMouseDragged -= AffectRotation;
        PlayerMouseMovementInput.OnMouseReleased -= ResetStartAffectValue;
        PlayerMouseMovementInput.OnMouseClicked -= SetStartAffectValue;
    }

    private void SetStartAffectValue()
    {
        _startYLocalRotation = _targetYLocalRotation;
    }

    private void ResetStartAffectValue()
    {
        _startYLocalRotation = 0;
    }

    private void AffectRotation(float amount)
    {
        float affectValue = -amount / PlayerMouseMovementInput.StrafeLimit * InputToRotationValueFactor;

        _targetYLocalRotation = Mathf.Clamp(_startYLocalRotation + affectValue, -MaxRotateLimit, MaxRotateLimit); ;
    }

    private void Rotate()
    {
        const float Speed = 2;

        float yLocalRotation = gameObject.transform.localRotation.y;
        Quaternion oldLocalRotation = gameObject.transform.localRotation;

        gameObject.transform.localRotation = new Quaternion(
            oldLocalRotation.x,
            Mathf.Lerp(yLocalRotation, _targetYLocalRotation, Time.deltaTime * Speed),
            oldLocalRotation.z,
            oldLocalRotation.w);
    }
}
