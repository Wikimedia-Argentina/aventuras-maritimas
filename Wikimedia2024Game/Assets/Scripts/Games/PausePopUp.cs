using UnityEngine;
using UnityEngine.Events;

public class PausePopUp : BasicPopup
{
    [SerializeField] private SoundSettings soundSettings;

    public UnityEvent OnBackToHomeClickEvent { get; private set; }

    protected override void InitializePopup()
    {
        OnBackToHomeClickEvent = new UnityEvent();
    }

    public override void Show()
    {
        base.Show();
        soundSettings.Show();
    }

    public override void Hide()
    {
        soundSettings.Hide();
        base.Hide();
    }

    public void OnBackToHomeClick()
    {
        ShowConfirmExitPopup();
    }

    public override void OnCloseButtonClick()
    {
        OnCloseButtonClickEvent.Invoke();
    }

    public override void ConfirmExitPopupConfirm()
    {
        OnBackToHomeClickEvent.Invoke();
    }
}
