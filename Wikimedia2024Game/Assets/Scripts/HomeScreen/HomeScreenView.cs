using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenView : MonoBehaviour
{
    private HomeScreenPresenter presenter;

    [SerializeField] private SettingsPopUp settingsPopUp;
    [SerializeField] private CreditsPopup creditsPopUp;
    [SerializeField] private AboutMuseumPopup aboutMuseumPopUp;
    [SerializeField] private AboutWikiPopup aboutWikiPopUp;

    void Start()
    {
        presenter = new HomeScreenPresenter(this);
        settingsPopUp.Hide();
        creditsPopUp.Hide();
        aboutMuseumPopUp.Hide();
        aboutWikiPopUp.Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPopUp.IsShowing)
                settingsPopUp.OnCloseButtonClick();
            else if (creditsPopUp.IsShowing)
                creditsPopUp.OnCloseButtonClick();
            else if (aboutMuseumPopUp.IsShowing)
                aboutMuseumPopUp.OnCloseButtonClick();
            else if (aboutWikiPopUp.IsShowing)
                aboutWikiPopUp.OnCloseButtonClick();
            else
                CloseApp();
        }
    }

    public void PlayGame(string gameId)
    {
        presenter.GoToPlayGame(gameId);
    }

    public void ShowSettingsPopup()
    {
        settingsPopUp.Show();
        settingsPopUp.OnCloseButtonClickEvent.AddListener(CloseSettings);
    }
    public void ShowCreditsPopup()
    {
        creditsPopUp.Show();
        creditsPopUp.OnCloseButtonClickEvent.AddListener(CloseCredits);
    }

    public void ShowAboutMuseumPopup()
    {
        aboutMuseumPopUp.Show();
        aboutMuseumPopUp.OnCloseButtonClickEvent.AddListener(CloseAboutMuseum);
    }

    public void ShowAboutWikiPopup()
    {
        aboutWikiPopUp.Show();
        aboutWikiPopUp.OnCloseButtonClickEvent.AddListener(CloseAboutWiki);
    }

    private void CloseSettings()
    {
        settingsPopUp.Hide();
        settingsPopUp.OnCloseButtonClickEvent.RemoveAllListeners();
    }

    private void CloseCredits()
    {
        creditsPopUp.Hide();
        creditsPopUp.OnCloseButtonClickEvent.RemoveAllListeners();
    }

    private void CloseAboutMuseum()
    {
        aboutMuseumPopUp.Hide();
        aboutMuseumPopUp.OnCloseButtonClickEvent.RemoveAllListeners();
    }

    private void CloseAboutWiki()
    {
        aboutWikiPopUp.Hide();
        aboutWikiPopUp.OnCloseButtonClickEvent.RemoveAllListeners();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
