using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HorizontalLayoutGroup))]
[RequireComponent (typeof(RectTransform))]

public class ShopContentResizer : MonoBehaviour
{
    [SerializeField] private RectTransform _parentRectTransform;
    [SerializeField] private RectTransform _cellRectTransformFromTemplate;

    private HorizontalLayoutGroup _layoutGroup;
    private RectTransform _rectTransform;
    
    private void OnEnable()
    {
        _rectTransform = GetComponent<RectTransform>();
        _layoutGroup = GetComponent<HorizontalLayoutGroup>();
        StartCoroutine(ResizeAfterDelay());
    }

    public void Resize()
    {
        int childCount = transform.childCount;
        int spacingsCount = childCount - 1;

        float paddingAndSpacingWidth = 
            _layoutGroup.padding.left + _layoutGroup.padding.right + _layoutGroup.spacing * spacingsCount;
        float targetRectRightValue = 
            childCount * _cellRectTransformFromTemplate.sizeDelta.x + paddingAndSpacingWidth;

        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, targetRectRightValue);
    }

    private IEnumerator ResizeAfterDelay()
    {
        const float delay = .05f;

        yield return new WaitForSeconds(delay);

        Resize();
    } 
}
