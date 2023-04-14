using UnityEngine;
using UnityEngine.UI;

public class ObstacleSign : MonoBehaviour
{
    [SerializeField] private MeshRenderer _sign;

    private Color _defaultImageColor;
    private Indicator _indicator;

    private void OnEnable()
    {
        _indicator = GetComponentInChildren<Indicator>();
        _defaultImageColor = _sign.material.color;
        ColorUpdate(0);
    }

    public void ColorUpdate(float transparencyFactor)
    {
        transparencyFactor = Mathf.Clamp01(transparencyFactor);

        _sign.material.color = new(_defaultImageColor.r, _defaultImageColor.g, _defaultImageColor.b, transparencyFactor);

        if (transparencyFactor < 1 )
        {
            if (_indicator.IsPlaying)
                _indicator.StopBlinking();

            return;
        }
        
        if (_indicator.IsPlaying == false)
            _indicator.RestartBlinking();
    }
}