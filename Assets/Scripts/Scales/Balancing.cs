using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancing : MonoBehaviour
{
    [SerializeField] private float _balanceFactor = 0.7f;
    [SerializeField] private float _balanceSpeed = 1;
    [SerializeField] private float _yOffset = -2f;
    [Range(3.5f, 5f)]
    [SerializeField] private float _basketSpawnXOffset = 4.2f;

    private float _currentBalanceValue = 0;
    private float _targetBalanceValue = 0;

    private Basket _basketLeft;
    private Basket _basketRight;

    private Coroutine _balancingJob;

    private void OnDisable()
    {
        StopBalancing();
    }

    public void SetBaskets(Basket left, Basket right)
    {
        _basketLeft = left;
        _basketRight = right;
        MoveBasketsByValue(0);
    }

    public void SetBasketsPositions(float scalesValue)
    {
        _targetBalanceValue = scalesValue * _balanceFactor;
        StartBalancing();
    }

    private void StopBalancing()
    {
        if (_balancingJob != null)
            StopCoroutine(_balancingJob);
    }

    private void StartBalancing()
    {
        StopBalancing();
        _balancingJob = StartCoroutine(Balance());
    }

    private void MoveBasketsByValue(float balanceValue)
    {
        _basketLeft.transform.localPosition = new Vector3(-_basketSpawnXOffset, _yOffset + balanceValue, 0);
        _basketRight.transform.localPosition = new Vector3(_basketSpawnXOffset, _yOffset - balanceValue, 0);
    }

    private IEnumerator Balance()
    {
        const float TimeInterval = 0.02f;

        while (_currentBalanceValue != _targetBalanceValue)
        {
            _currentBalanceValue = Mathf.Lerp(_currentBalanceValue, _targetBalanceValue, Time.deltaTime * _balanceSpeed);
            MoveBasketsByValue(_currentBalanceValue);
            yield return new WaitForSeconds(TimeInterval);
        }
    }
}
