using UnityEngine;

public class UiCornersMover : MonoBehaviour
{
    [SerializeField] private float _moveYPosition = -200;
    [SerializeField] private int _screenWidthThreshold = 1600;
    [SerializeField] private RectTransform[] _corners;

    private void OnEnable()
    {
        UiScaleResizer.OnScreenSizeChanged += UpdateCornersPosition;
    }

    private void OnDisable()
    {
        UiScaleResizer.OnScreenSizeChanged -= UpdateCornersPosition;
    }

    private void UpdateCornersPosition()
    {
        if (Screen.height > Screen.width && Screen.width < _screenWidthThreshold)
            MoveCorners(_moveYPosition);
        else
            MoveCorners(0);
    }

    private void MoveCorners(float newYPosition)
    {
        for (int i = 0; i < _corners.Length; i++)
            _corners[i].sizeDelta = new Vector2(0, newYPosition);
    }
}