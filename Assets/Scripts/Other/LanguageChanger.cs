using UnityEngine;

public class LanguageChanger : MonoBehaviour
{
    public void ChangeLanguage(Localization.LanguageType language)
    {
        Localization.ChangeLocalization(language);
    }
}