using TMPro;
using UnityEngine;

public class BasketCounters : MonoBehaviour
{
    [SerializeField] private Scales _scales;
    [SerializeField] private TextMeshProUGUI _leftText;
    [SerializeField] private TextMeshProUGUI _rightText;
    [SerializeField] private TextMeshProUGUI _differenceText;
    [Header("Color Indicators")]
    [SerializeField] private int _basketOverloadPaintMinThreshold;
    [SerializeField] private Color _default;
    [SerializeField] private Color _basketOverload;
    [SerializeField] private Color _obstacleThreat;

    private int _leftCount;
    private int _rightCount;
    private int _difference;
    private bool _isObstacleThreat;
    private float _basketOverloadLimitValue;

    private Basket _leftBasket;
    private Basket _rightBasket;

    private void Start()
    {
        _isObstacleThreat = false;
        _leftCount = 0;
        _rightCount = 0;
        _difference = 0;
        _basketOverloadLimitValue = Scales.MaxAmplitude / _scales.WeightDifferenceFactor;
        _leftBasket = _scales.LeftBasket;
        _leftBasket.OnWeightChanged += UpdateCounts;
        _rightBasket = _scales.RightBasket;
        _rightBasket.OnWeightChanged += UpdateCounts;
        _scales.LeftScaner.OnObstacleFound += OnObstacleFoundEvent;
        _scales.RightScaner.OnObstacleFound += OnObstacleFoundEvent;
        _scales.LeftScaner.OnNoObstacleFound += OnObstacleLostEvent;
        _scales.RightScaner.OnNoObstacleFound += OnObstacleLostEvent;
        UpdateTexts();
    }

    private void OnDisable()
    {
        _leftBasket.OnWeightChanged -= UpdateCounts;
        _rightBasket.OnWeightChanged -= UpdateCounts;
        _scales.LeftScaner.OnObstacleFound -= OnObstacleFoundEvent;
        _scales.RightScaner.OnObstacleFound -= OnObstacleFoundEvent;
        _scales.LeftScaner.OnNoObstacleFound -= OnObstacleLostEvent;
        _scales.RightScaner.OnNoObstacleFound -= OnObstacleLostEvent;
    }

    private void UpdateCounts()
    {
        _leftCount = _leftBasket.CargoCount;
        _rightCount = _rightBasket.CargoCount;
        _difference = _leftCount - _rightCount;
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        if (_difference == 0)
            _differenceText.text = "";
        else if (_difference > 0)
            _differenceText.text = "> " + _difference.ToString() + " >";
        else
            _differenceText.text = "< " + Mathf.Abs(_difference).ToString() + " <";

        RePaintDifferenceText();
        _leftText.text = _leftCount.ToString();
        _rightText.text = _rightCount.ToString();
    }

    private void RePaintDifferenceText()
    {
        if (_isObstacleThreat)
            _differenceText.color = _obstacleThreat;
        else
            _differenceText.color = _default;

        PaintBasketCount(_leftText, _difference);
        PaintBasketCount(_rightText, -_difference);
    }

    private void PaintBasketCount(TextMeshProUGUI basketCounterText, int difference)
    {
        if (difference < _basketOverloadPaintMinThreshold)
            basketCounterText.color = _default;
        else if (difference < _basketOverloadLimitValue)
            basketCounterText.color = Color.Lerp(_default, _basketOverload, difference / _basketOverloadLimitValue);
        else
            basketCounterText.color = _basketOverload;
    }

    private void OnObstacleFoundEvent()
    {
        _isObstacleThreat = true;
        RePaintDifferenceText();
    }

    private void OnObstacleLostEvent()
    {
        _isObstacleThreat = false;
        RePaintDifferenceText();
    }
}