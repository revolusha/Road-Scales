using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TESTDebuggingLabels : MonoBehaviour
{
    private static TESTDebuggingLabels _debugging;
    private static Transform _template;

    private static TextMeshProUGUI[] _labels;

    public static TESTDebuggingLabels Instance => _debugging;

    private void OnEnable()
    {
        if (_debugging == null)
            _debugging = this;
    }

    private void Start()
    {
        TextMeshProUGUI template = GetComponentInChildren<TextMeshProUGUI>();

        _template = template.transform;
        _labels = new TextMeshProUGUI[] { _template.GetComponent<TextMeshProUGUI>() };
    }

    public static void ShowMessage(int labelIndex, string message)
    {
        if (labelIndex < 0 || _labels == null)
        {
            throw new System.ArgumentOutOfRangeException();
        }

        if (_labels.Length <= labelIndex)
        {
            Instance.ExpandArray(labelIndex);
        }

        if (_labels[labelIndex] == null)    
            Instance.CreateNewLabel(labelIndex);

        _labels[labelIndex].text = message;
    }
    private void CreateNewLabel(int labelIndex)
    {
        float labelHeight = CalculateHeightFromAnchors(
            _labels[0].rectTransform.anchorMin.y,
            _labels[0].rectTransform.anchorMax.y);

        Vector3 anchoredPosition = _labels[0].rectTransform.anchoredPosition;
        Vector3 newAnchoredPosition = new(
            anchoredPosition.x, 
            anchoredPosition.y + labelHeight * labelIndex, 
            anchoredPosition.z);

        TextMeshProUGUI newLabel = Instantiate(
            _template.gameObject,
            Vector3.zero,
            Quaternion.identity,
            Instance.transform)
            .GetComponent<TextMeshProUGUI>();

        newLabel.rectTransform.anchoredPosition = newAnchoredPosition;

        Debug.Log(_labels[0].rectTransform.sizeDelta.y.ToString());

        _labels[labelIndex] = newLabel;
        return ;
    }

    private void ExpandArray(int desiredIndex)
    {
        TextMeshProUGUI[] newArray = new TextMeshProUGUI[desiredIndex + 1];

        for (int i = 0; i < _labels.Length; i++)
        {
            newArray[i] = _labels[i];
        }

        _labels = newArray;
    }

    private float CalculateHeightFromAnchors(float anchor1, float anchor2)
    {
        float factor = anchor1 - anchor2;

        return Screen.height * factor;
    }
}
