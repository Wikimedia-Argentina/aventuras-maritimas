using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicTextField : MonoBehaviourWithContext
{
    [SerializeField] private string TID;
    [SerializeField] private List<string> parameters;

    private void Awake()
    {
        LoadTexts();
        MyLocalization.OnLangChanged.AddListener(LoadTexts);
    }

    private void LoadTexts()
    {
        TMP_Text textField = gameObject.GetComponent<TMP_Text>();
        textField.text = MyLocalization.GetLocalizedText(TID);

        for (int i = 0; i < parameters.Count; i++)
        {
            textField.text = textField.text.Replace("{" + i + "}", parameters[i]);
        }
    }
}
