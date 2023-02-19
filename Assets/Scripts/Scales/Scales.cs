using UnityEngine;

public class Scales : MonoBehaviour
{
    [SerializeField] private float _yOffset = -2f;

    private const float MaxAmplitude = 2;
    private const float WeightDifferenceFactor = .1f;

    private float _scalesValue = 0;

    private Basket _basketLeft;
    private Basket _basketRight;

    private void OnDisable()
    {
        if (_basketLeft != null)
            _basketLeft.OnWeightChanged -= CheckBasketsWeights;

        if (_basketRight != null)
            _basketRight.OnWeightChanged -= CheckBasketsWeights;
    }

    public void SetBaskets(Basket left, Basket right)
    {
        _basketLeft = left;
        _basketRight = right;
        BalanceBaskets();
        _basketLeft.OnWeightChanged += CheckBasketsWeights;
        _basketRight.OnWeightChanged += CheckBasketsWeights;
    }

    private void CheckBasketsWeights()
    {
        _scalesValue = (_basketRight.Weight - _basketLeft.Weight) * WeightDifferenceFactor;
        BalanceBaskets();
    }

    private void BalanceBaskets()
    {
        _scalesValue = Mathf.Clamp(_scalesValue, -MaxAmplitude, MaxAmplitude);

        _basketLeft.transform.localPosition = 
            _basketLeft.transform.localPosition + new Vector3(0, _yOffset + _scalesValue, 0);
        _basketRight.transform.localPosition = 
            _basketRight.transform.localPosition + new Vector3(0, _yOffset - _scalesValue, 0);
    }
}
