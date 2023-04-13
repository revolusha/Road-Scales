using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(RectTransform))]

public class ShopContentResizer : MonoBehaviour
{
    [SerializeField] private bool _isVertical;
    [SerializeField] private RectTransform _parentRectTransform;
    [SerializeField] private RectTransform _childRectTransformFromTemplate;

    private HorizontalLayoutGroup _horizontalLayoutGroup;
    private VerticalLayoutGroup _verticalLayoutGroup;
    private RectTransform _rectTransform;
    
    private void OnEnable()
    {
        Initialize();
        _rectTransform = GetComponent<RectTransform>();
        _horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        StartCoroutine(ResizeAfterDelay());
    }

    private void Initialize()
    {
        if (TryGetComponent(out _horizontalLayoutGroup))
        {
            _isVertical = false;
            return;
        }

        if (TryGetComponent(out _verticalLayoutGroup))
        {
            _isVertical = true;
            return;
        }

        throw new System.Exception("Shop resizing error!");
    }

    public void ResizeVertical()
    {
        int childCount = transform.childCount;
        int spacingsCount = childCount - 1;

        float paddingAndSpacingHeight =
            _verticalLayoutGroup.padding.top + _verticalLayoutGroup.padding.bottom + _verticalLayoutGroup.spacing * spacingsCount;
        float targetRectBottomValue =
            childCount * _childRectTransformFromTemplate.sizeDelta.y + paddingAndSpacingHeight;

        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, targetRectBottomValue);
    }

    public void ResizeHorizontal()
    {
        int childCount = transform.childCount;
        int spacingsCount = childCount - 1;

        float paddingAndSpacingWidth =
            _horizontalLayoutGroup.padding.left + _horizontalLayoutGroup.padding.right + _horizontalLayoutGroup.spacing * spacingsCount;
        float targetRectRightValue =
            childCount * _childRectTransformFromTemplate.sizeDelta.x + paddingAndSpacingWidth;

        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetRectRightValue);
    }

    private IEnumerator ResizeAfterDelay()
    {
        const float delay = .05f;

        yield return new WaitForSeconds(delay);

        if (_isVertical)
            ResizeVertical();
        else
            ResizeHorizontal();
    } 
}