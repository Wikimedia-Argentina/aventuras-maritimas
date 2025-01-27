using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class Localization : MonoBehaviour
{
    private const string DEFAULT_LANG = "EN";
    private const string Key_CurrentLang = "lang";
    private static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";

    public UnityEvent OnLangChanged;
    public string CurrentLang { get; private set; }

    private Dictionary<string, Dictionary<string, string>> texts;

    public string GetLocalizedText(string TID)
    {
        if (texts[CurrentLang].ContainsKey(TID))
            return texts[CurrentLang][TID];
        else
            return "Missing TID " + TID;
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey(Key_CurrentLang))
            CurrentLang = PlayerPrefs.GetString(Key_CurrentLang);
        else
            CurrentLang = GetDefaultLanguage();

        ParseLocalizationFile();
    }

    private void ParseLocalizationFile()
    {
        var csvPlain = Resources.Load<TextAsset>(FileName());
        var csvLines = Regex.Split(csvPlain.text, LINE_SPLIT_RE);

        var resultsES = new Dictionary<string, string>();
        var resultsEN = new Dictionary<string, string>();

        for (var i = 1; i < csvLines.Length; i++)
        {
            var lineColumns = csvLines[i].Split(FieldSplitChar());

            resultsES.Add(lineColumns[0], ProcessText(lineColumns[1]));
            resultsEN.Add(lineColumns[0], ProcessText(lineColumns[2]));
        }

        texts = new Dictionary<string, Dictionary<string, string>>();
        texts.Add("ES", resultsES);
        texts.Add("EN", resultsEN);
    }

    private string ProcessText(string input)
    {
        while (input.Contains("[]"))
            input = input.Replace("[]", "\n");

        while (input.Contains("[.]"))
            input = input.Replace("[.]", ",");

        return input;
    }

    private char FieldSplitChar()
    {
        return ',';
    }

    private string FileName()
    {
        return "locale";
    }

    private string GetDefaultLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                return "EN";
            case SystemLanguage.Spanish:
                return "ES";
            default:
                return DEFAULT_LANG;
        }
    }

    public void SetLanguage(string langToSet)
    {
        CurrentLang = langToSet;

        PlayerPrefs.SetString(Key_CurrentLang, CurrentLang);
        PlayerPrefs.Save();

        OnLangChanged.Invoke();
    }
}