using System;
using UnityEngine;
using UnityEngine.Events;

public class BasicPopup : MonoBehaviourWithContext
{
    public UnityEvent OnCloseButtonClickEvent { get; private set; }

    public GameObject ConfirmExitPopup;

    public bool IsShowing { get; private set; }

    private void Awake()
    {
        if(ConfirmExitPopup != null)
            ConfirmExitPopup.SetActive(false);

        OnCloseButtonClickEvent = new UnityEvent();
        InitializePopup();
    }

    protected virtual void InitializePopup()
    {

    }

    protected void ShowConfirmExitPopup()
    {
        ConfirmExitPopup.SetActive(true);
    }

    public virtual void ConfirmExitPopupConfirm()
    {
        throw new NotImplementedException();
    }

    public void ConfirmExitPopupCancel()
    {
        ConfirmExitPopup.SetActive(false);
    }

    public virtual void Show()
    {
        IsShowing = true;
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        IsShowing = false;
    }

    public virtual void OnCloseButtonClick()
    {
        throw new NotImplementedException();
    }
}
