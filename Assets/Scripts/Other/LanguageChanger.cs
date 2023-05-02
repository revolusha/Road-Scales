using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public void ChangeLanguage(int index)
    {
        switch (index)
        {
            case 0:
                ChangeLanguage(Localization.LanguageType.Russian);
                break;

            case 1:
                ChangeLanguage(Localization.LanguageType.English);
                break;

            case 2:
                ChangeLanguage(Localization.LanguageType.Turkish);
                break;

            default:
                throw new System.IndexOutOfRangeException();
        }
    }

    private void ChangeLanguage(Localization.LanguageType language)
    {
        Localization.ChangeLocalization(language);
    }
}