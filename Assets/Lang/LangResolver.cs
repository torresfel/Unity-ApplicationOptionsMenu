using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LangResolver : MonoBehaviour
{
    private const char Separator = '=';
    private readonly static Dictionary<string, string> _lang = new Dictionary<string, string>();
    private SystemLanguage _language;
    [SerializeField] private Dropdown languageSelector ;

    private void Awake()
    {
        ChangeLanguage();
        DontDestroyOnLoad(gameObject);
        ReadProperties();
    }

    public void ChangeLanguage()
    {
        switch (languageSelector.value)
        {
            case 0:
                //English
                _language = SystemLanguage.English;
                break;
            case 1:
                //Japanese
                _language = SystemLanguage.Japanese;
                break;
            default:
                _language = SystemLanguage.English;
                break;
        }
        ReadProperties();
        ResolveTexts();
    }

    private void ReadProperties()
    {
        var file = Resources.Load<TextAsset>(_language.ToString());
        foreach (var line in file.text.Split('\n'))
        {
            var prop = line.Split(Separator);
            _lang[prop[0]] = prop[1];
        }
    }

    public void ResolveTexts()
    {
        var allTexts = Resources.FindObjectsOfTypeAll<LangText>();
        foreach (var langText in allTexts)
        {
            var text = langText.GetComponent<Text>();
            if(text.GetComponent<LangText>() != null)
                text.text = Regex.Unescape(_lang[langText.Identifier]);
        }
        var allDropDown = Resources.FindObjectsOfTypeAll<LangDropdown>();
        foreach (var langDropdown in allDropDown)
        {
            var dropdown = langDropdown.GetComponent<Dropdown>();
            var previousValue = dropdown.value;
            dropdown.ClearOptions();
            foreach(var optionIdentifier in langDropdown.optionsIdentifier)
            {
                var optionData = new Dropdown.OptionData(Regex.Unescape(_lang[optionIdentifier]));
                dropdown.options.Add(optionData);
            }
            dropdown.value = previousValue;
            dropdown.RefreshShownValue();
        }
    }
}
