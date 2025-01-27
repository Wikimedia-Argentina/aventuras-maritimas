using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutMuseumPopup : BasicPopup
{
    public override void OnCloseButtonClick()
    {
        OnCloseButtonClickEvent.Invoke();
    }

    public void MoreInfoCLick()
    {
        Application.OpenURL(Links.MuseumWikiPage);
    }
}
