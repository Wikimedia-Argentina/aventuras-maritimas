using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainObjectFoundPopupUp : BasicPopup
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text descriptionTxt;

    private string linkToNavigate;

    public override void OnCloseButtonClick()
    {
        OnCloseButtonClickEvent.Invoke();
    }

    public void MoreInfoButtonClick()
    {
        if(!string.IsNullOrEmpty(linkToNavigate))
            Application.OpenURL(linkToNavigate);
    }

    internal void Show(ObjectFullData data)
    {
        titleTxt.text = data.Title;
        descriptionTxt.text = data.Description;
        image.sprite = Resources.Load<Sprite>(data.ImgPath);
        linkToNavigate = data.MoreInfoLink;

        base.Show();
    }
}
