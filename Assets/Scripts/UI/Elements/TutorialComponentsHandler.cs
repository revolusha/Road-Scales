using UnityEngine;
using TMPro;
using System;

public class TutorialComponentsHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tutorialsCounter;
    [SerializeField] private LocalizationString _localizedStringComponent;
    [SerializeField] private TutorialButton _button;
    [SerializeField] private GameObject _shopArrow;
    [SerializeField] private LocalizationStringValue[] _localizedStrings;

    private int _currentIndex;

    public static Action OnTutorialFinished;

    private void Start()
    {
        SwitchKeyString(0);
    }

    public void SwitchToNextKeyString()
    {
        ++_currentIndex;

        if (_currentIndex >= _localizedStrings.Length)
        {
            OnTutorialFinished?.Invoke();
            return;
        }

        SwitchKeyString(_currentIndex);
    }

    private void SwitchKeyString(int index)
    {
        const string Separator = " / ";
        const int IndexOffset = 1;

        _currentIndex = index;
        _tutorialsCounter.text = (index + IndexOffset).ToString() + Separator + _localizedStrings.Length.ToString();
        _localizedStringComponent.StringReference = _localizedStrings[index];
        _localizedStringComponent.UpdateText();
        _button.HideButton();

        if (index == _localizedStrings.Length - 1)
            _shopArrow.SetActive(true);
    }
}