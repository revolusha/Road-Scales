public class LocalizationStringWithDynamicValue : LocalizationString
{
    private string _dynamicValue;

    public void ChangeValue(string value)
    {
        _dynamicValue = value;
        ChangeLanguage(Localization.Language);
    }

    protected override void ChangeLanguage(Localization.LanguageType language)
    {
        switch (language)
        {
            case Localization.LanguageType.Russian:
                _textComponent.text = StringReference.RuString + _dynamicValue;
                break;

            case Localization.LanguageType.English:
                _textComponent.text = StringReference.EnString + _dynamicValue;
                break;

            case Localization.LanguageType.Turkish:
                _textComponent.text = StringReference.TrString + _dynamicValue;
                break;
        }
    }
}