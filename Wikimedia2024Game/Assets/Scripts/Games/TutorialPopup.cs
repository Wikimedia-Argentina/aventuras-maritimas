using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPopup : BasicPopup
{
    [SerializeField] protected GameObject TutoVideo;
    [SerializeField] protected GameObject TutoCam;

    protected override void InitializePopup()
    {
        ReproduceTutorialAnim();
    }

    protected virtual void ReproduceTutorialAnim()
    {
    }

    public override void OnCloseButtonClick()
    {
        CloseTutorial();

        MySoundManager.StopAllSounds();
        OnCloseButtonClickEvent.Invoke();
    }

    protected virtual void CloseTutorial()
    {
        if(TutoVideo != null)
            TutoVideo.SetActive(false);

        if(TutoCam != null)
            TutoCam.SetActive(false);
    }
}
