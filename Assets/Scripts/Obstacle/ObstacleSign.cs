using UnityEngine;
using UnityEngine.UI;

public class ObstacleSign : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Image _image;

    private Color _defaultLineColor;
    private Color _defaultImageColor;

    private void OnEnable()
    {
        _defaultImageColor = _image.color;
        _defaultLineColor = _lineRenderer.endColor;
        ColorUpdate(0);
    }

    public void ColorUpdate(float transparencyFactor)
    {
        transparencyFactor = Mathf.Clamp01(transparencyFactor);

        _image.color = new(_defaultImageColor.r, _defaultImageColor.g, _defaultImageColor.b, transparencyFactor);
        _lineRenderer.startColor = new(_defaultLineColor.r, _defaultLineColor.g, _defaultLineColor.b, transparencyFactor);
        _lineRenderer.endColor = _lineRenderer.startColor;
    }
}