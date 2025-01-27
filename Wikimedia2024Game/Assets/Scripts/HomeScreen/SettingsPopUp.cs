using TMPro;
using UnityEngine;

public class SettingsPopUp : BasicPopup
{
    [SerializeField] private SoundSettings soundSettings;
    [SerializeField] private TMP_Text versionNumber;

    public override void Show()
    {
        base.Show();
        versionNumber.text = "Versión " + Application.version;
        soundSettings.Show();
    }

    public override void Hide()
    {
        soundSettings.Hide();
        base.Hide();
    }

    public override void OnCloseButtonClick()
    {
        OnCloseButtonClickEvent.Invoke();
    }

    public void OnTermsAndConditionsClick()
    {
        Application.OpenURL(Links.TermsAndConditions);
    }
}
