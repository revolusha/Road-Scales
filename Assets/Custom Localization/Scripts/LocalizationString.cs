using UnityEngine;
using TMPro;

public class LocalizationString : MonoBehaviour
{
    [SerializeField] protected TMP_Text _textComponent;

    public LocalizationStringValue StringReference;

    private void OnEnable()
    {
        Localization.OnLocalizationChanged += ChangeLanguage; 
        UpdateText();
    }

    private void OnDisable()
    {
        Localization.OnLocalizationChanged -= ChangeLanguage;
    }

    public void UpdateText()
    {
        ChangeLanguage(Localization.Language);
    }

    protected virtual void ChangeLanguage(Localization.LanguageType language)
    {
        switch (language)
        {
            case Localization.LanguageType.Russian:
                _textComponent.text = StringReference.RuString;
                break;

            case Localization.LanguageType.English:
                _textComponent.text = StringReference.EnString;
                break;

            case Localization.LanguageType.Turkish:
                _textComponent.text = StringReference.TrString;
                break;
        }
    }
}