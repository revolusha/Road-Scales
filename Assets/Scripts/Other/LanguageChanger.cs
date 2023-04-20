using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageChanger : MonoBehaviour
{
    private bool _isActive = false;

    public void ChangeLanguage(int localeIndex)
    {
        if (_isActive)
            return;

        StartCoroutine(SetLocale(localeIndex));
    }

    private IEnumerator SetLocale(int index)
    {
        _isActive = true;

        yield return LocalizationSettings.InitializationOperation;

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        _isActive = false;
    }
}