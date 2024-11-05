using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField]
    private LocalizedStringDatabase stringTable;

    private void Start()
    {
        Constants.StringTable = stringTable;
        StartCoroutine(SetLocaleBasedOnDeviceLanguage());
    }

    private IEnumerator SetLocaleBasedOnDeviceLanguage()
    {
        yield return LocalizationSettings.InitializationOperation;

        var systemLanguage = Application.systemLanguage;

        Locale selectedLocale = null;
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.Code.Equals(systemLanguage.ToString(), System.StringComparison.OrdinalIgnoreCase))
            {
                selectedLocale = locale;
                break;
            }
        }

        if (selectedLocale == null)
        {
            Debug.LogWarning("No exact locale match found. Setting default to English.");
            selectedLocale = LocalizationSettings.AvailableLocales.GetLocale("en"); // Set default to English or any preferred language
        }

        LocalizationSettings.SelectedLocale = selectedLocale;
        Debug.Log("Locale set to: " + selectedLocale.LocaleName);
        Constants.CurrentLanguage = selectedLocale.ToString().Contains("en") ? Language.English : Language.Spanish;
    }
}
