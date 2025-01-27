using UnityEngine;

public class CreditsPopup : BasicPopup
{
    public override void OnCloseButtonClick()
    {
        OnCloseButtonClickEvent.Invoke();
    }

    public void BichoRaroClick()
    {
        Application.OpenURL(Links.BichoRaroWebPage);
    }

    public void WikimediaClick()
    {
        Application.OpenURL(Links.WikimediaWebPage);
    }

    public void CodCulturaClick()
    {
        Application.OpenURL(Links.InfoCodigoCultura);
    }

    public void BancoClick()
    {
        Application.OpenURL(Links.MuseumWebPage);
    }
}
