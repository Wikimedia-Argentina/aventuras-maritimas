using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOverPopup : BasicPopup
{
    [SerializeField] private GameObject winGO;
    [SerializeField] private GameObject looseGO;

    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    [SerializeField] private TMP_Text resultTip;

    public UnityEvent OnPlayAgainButtonClickEvent { get; private set; }
    public UnityEvent OnContinueButtonClickEvent { get; private set; }
    public UnityEvent OnMoreInfoButtonClickEvent { get; private set; }

    public virtual void Show(bool isWin, int points, int stars, string resultText)
    {
        winGO.SetActive(isWin);
        looseGO.SetActive(!isWin);

        resultTip.text = resultText;

        star1.SetActive(stars >= 1);
        star2.SetActive(stars >= 2);
        star3.SetActive(stars >= 3);

        if(stars == 0)
        {
            MySoundManager.PlaySfxSound("Sound/SFXNoStars");
        }
        if(stars == 3)
        {
            MySoundManager.PlaySfxSound("Sound/SFXAllStars");
        }

        base.Show();
    }

    protected override void InitializePopup()
    {
        OnPlayAgainButtonClickEvent = new UnityEvent();
        OnContinueButtonClickEvent = new UnityEvent();
        OnMoreInfoButtonClickEvent = new UnityEvent();
    }

    public void OnPlayAgainButtonClick()
    {
        OnPlayAgainButtonClickEvent.Invoke();
    }

    public void OnContinueButtonClick()
    {
        OnContinueButtonClickEvent.Invoke();
    }

    public void OnMoreInfoButtonClick()
    {
        OnMoreInfoButtonClickEvent.Invoke();
    }

    public override void OnCloseButtonClick()
    {
        ShowConfirmExitPopup();
    }

    public override void ConfirmExitPopupConfirm()
    {
        OnCloseButtonClickEvent.Invoke();
    }
}
