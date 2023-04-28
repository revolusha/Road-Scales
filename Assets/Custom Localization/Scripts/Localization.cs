using System;

public static class Localization
{
    public static Action<LanguageType> OnLocalizationChanged;

    public enum LanguageType
    {
        Russian,
        English,
        Turkish
    }

    public static LanguageType Language { get; private set; }

    public static void ChangeLocalization(LanguageType language)
    {
        Language = language;
        OnLocalizationChanged?.Invoke(language);
    }
}