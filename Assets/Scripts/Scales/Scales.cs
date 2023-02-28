using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Balancing))]

public class Scales : MonoBehaviour
{
    [SerializeField] private GameObject _basketTemplate;
    [SerializeField] private float _weightDifferenceFactor = .1f;

    private const float MaxAmplitude = 2.5f;

    private float _scalesValue = 0;

    private Basket _basketLeft;
    private Basket _basketRight;
    private Balancing _balancing;

    public static Action OnScalesBroke;

    public int AllCatchedCargo => _basketLeft.CargoCount + _basketRight.CargoCount;

    private void OnEnable()
    {
        _balancing = GetComponent<Balancing>();

        _basketLeft = Instantiate(_basketTemplate, transform.position, Quaternion.identity, transform)
            .GetComponent<Basket>();
        _basketRight = Instantiate(_basketTemplate, transform.position, Quaternion.identity, transform)
            .GetComponent<Basket>();

        SetBaskets();
    }

    private void OnDisable()
    {
        UnsubscribeFromActions();
    }

    public void FinishBalancingAndSummarize()
    {
        UnsubscribeFromActions();
        GameData.Money.GetReward(AllCatchedCargo);
    }

    private void SetBaskets()
    {
        _basketLeft.OnWeightChanged += CheckBasketsWeights;
        _basketRight.OnWeightChanged += CheckBasketsWeights;
        _basketLeft.OnTouchedByObstacle += BecomeBroken;
        _basketRight.OnTouchedByObstacle += BecomeBroken;
        _balancing.SetBaskets(_basketLeft, _basketRight);
    }

    private void CheckBasketsWeights()
    {
        _scalesValue = (_basketRight.Weight - _basketLeft.Weight) * _weightDifferenceFactor;

        if (_scalesValue < -MaxAmplitude || _scalesValue > MaxAmplitude)
        {
            BecomeBroken();
            return;
        }

        TESTDebuggingLabels.ShowMessage(3, "scales " + _scalesValue.ToString());
        Balance();
    }

    private void UnsubscribeFromActions()
    {
        if (_basketLeft != null)
        {
            _basketLeft.OnWeightChanged -= CheckBasketsWeights;
            _basketLeft.OnTouchedByObstacle -= BecomeBroken;
        }

        if (_basketRight != null)
        {
            _basketRight.OnWeightChanged -= CheckBasketsWeights;
            _basketRight.OnTouchedByObstacle -= BecomeBroken;
        }
    }

    private void Balance()
    {
        _balancing.SetBasketsPositions(_scalesValue);
    }

    private void BecomeBroken()
    {
        UnsubscribeFromActions();
        OnScalesBroke?.Invoke();
    }
}
