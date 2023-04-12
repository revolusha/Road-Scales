using TMPro;
using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    private const string LabelText = "Level ";
    private const int IndexOffset = 1;

    private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        _textField = GetComponentInChildren<TextMeshProUGUI>();
        _textField.text = LabelText + (Game.LevelHandler.CurrentLevelIndex + IndexOffset).ToString();
    }
}