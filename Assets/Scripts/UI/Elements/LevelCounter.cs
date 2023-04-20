using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private LocalizedString _localeLevelKeyString;

    private const int IndexOffset = 1;

    private int _levelCount;

    private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        _textField = GetComponentInChildren<TextMeshProUGUI>();
        _localeLevelKeyString.Arguments = new object[] { _levelCount };
        _localeLevelKeyString.StringChanged += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        _localeLevelKeyString.StringChanged -= UpdateText;
    }

    private void UpdateText(string value)
    {
        _textField.text = value;
    }

    private void UpdateText()
    {
        _levelCount = Game.LevelHandler.CurrentLevelIndex + IndexOffset;
        _localeLevelKeyString.Arguments[0] = _levelCount;
        _localeLevelKeyString.RefreshString();
    }
}