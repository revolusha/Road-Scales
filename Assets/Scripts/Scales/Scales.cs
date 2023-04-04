using System;
using UnityEngine;

[RequireComponent(typeof(Balancing))]

public class Scales : MonoBehaviour
{
    [SerializeField] private Basket _basketLeft;
    [SerializeField] private Basket _basketRight;
    [SerializeField] private float _weightDifferenceFactor = 0.09f;

    public const float MaxAmplitude = 1.8f;

    private float _scalesValue = 0;

    private ObstacleScaner _scanerLeft;
    private ObstacleScaner _scanerRight;
    private Balancing _balancing;

    public static Action OnScalesBroke;

    public float WeightDifferenceFactor => _weightDifferenceFactor;
    public int AllCatchedCargo => _basketLeft.CargoCount + _basketRight.CargoCount;
    public ObstacleScaner LeftScaner => _scanerLeft;
    public ObstacleScaner RightScaner => _scanerRight;
    public Basket LeftBasket => _basketLeft;
    public Basket RightBasket => _basketRight;

    private void OnEnable()
    {
        _balancing = GetComponent<Balancing>();
        _scanerLeft = _basketLeft.GetComponentInChildren<ObstacleScaner>();
        _scanerRight = _basketRight.GetComponentInChildren<ObstacleScaner>();
        SetBaskets();
    }

    private void OnDisable()
    {
        UnsubscribeFromActions();
    }

    public void FinishBalancingAndSummarize()
    {
        UnsubscribeFromActions();
        Game.Money.GetReward(AllCatchedCargo);
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
        _scalesValue = (_basketRight.CargoCount - _basketLeft.CargoCount) * _weightDifferenceFactor;

        if (_scalesValue < -MaxAmplitude || _scalesValue > MaxAmplitude)
        {
            BecomeBroken();
            return;
        }

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
        Game.SoundPlayer.PlayScalesBreakSound();
    }
}
