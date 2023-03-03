using UnityEngine;

public class ObstacleWarning : MonoBehaviour
{
    [SerializeField] private Scales _scales;
    [SerializeField] private Indicator _leftIndicator;
    [SerializeField] private Indicator _rightIndicator;

    private ObstacleScaner _leftScaner;
    private ObstacleScaner _rightScaner;

    private void OnEnable()
    {
        _leftScaner = _scales.LeftScaner;
        _rightScaner = _scales.RightScaner;
        Scales.OnScalesBroke += TurnOffWarning;
        _leftScaner.OnObstacleFound += WarningLeftSide;
        _rightScaner.OnObstacleFound += WarningRightSide;
        _leftScaner.OnNoObstacleFound += TurnOffWarning;
        _rightScaner.OnNoObstacleFound += TurnOffWarning;        
    }

    private void OnDisable()
    {
        Scales.OnScalesBroke -= TurnOffWarning;
        _leftScaner.OnObstacleFound -= WarningLeftSide;
        _rightScaner.OnObstacleFound -= WarningRightSide;
        _leftScaner.OnNoObstacleFound -= TurnOffWarning;
        _rightScaner.OnNoObstacleFound -= TurnOffWarning;
    }

    private void WarningLeftSide()
    {
        _leftIndicator.ShowWarning();
        _rightIndicator.HideWarning();
    }

    private void WarningRightSide()
    {
        _rightIndicator.ShowWarning();
        _leftIndicator.HideWarning();
    }

    private void TurnOffWarning()
    {
        _leftIndicator.HideWarning();
        _rightIndicator.HideWarning();
    }
}
