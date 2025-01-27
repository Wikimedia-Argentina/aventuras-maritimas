using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceObjectGame : Game
{
    [SerializeField] private Camera gameCamera;

    [SerializeField] private TMP_Text debugText;
    [SerializeField] private Animator stagesAnimator;
    [SerializeField] private GameObject btnZoomIn;
    [SerializeField] private GameObject btnZoomOut;
    [SerializeField] private Hinter hint;

    [SerializeField] private int totalItemsToFind = 10;

    [SerializeField] private MainObjectFoundPopupUp mainObjectPlacedPopup;
    [SerializeField] private CoinInfoTooltip coinInfoTooltip;

    [SerializeField] private Transform[] itemSpawn;

    [SerializeField] private CLickforInfoObject[] onlyInfoObjects;

    [SerializeField] private ItemHolder[] allTheMandatoryItemHolders;
    [SerializeField] private PlaceableItem[] allTheMandatoryPlaceableItems;

    [SerializeField] private ItemHolder[] allTheOptionalItemHolders;
    [SerializeField] private PlaceableItem[] allTheOptionalPlaceableItems;

    [SerializeField] private GameObject punishBlocker;
    [SerializeField] private int failedAttemptsUntilPunish = 3;
    [SerializeField] private float timeOfPunishment = 3;

    private string[] SpecialObjects = {"mon_cuadrada", "mon_corazon", "mon_oro", "colgante"};

    private ItemHolder[] activeHolders;
    private PlaceableItem[] activePlaceableItems;
    private int maxAchievablePoints;
    private int totalPoints;
    private int failedAttemptsInARow = 0;

    protected override void StartGame()
    {
        mainObjectPlacedPopup.Hide();
        coinInfoTooltip.Hide();

        MySoundManager.PlayMusicLoop("Sound/PlaceObject/MusicPlaceObjects");

        debugText.text = "";

        btnZoomIn.SetActive(true);
        btnZoomOut.SetActive(false);

        foreach (var item in allTheOptionalItemHolders)
        {
            item.Deactivate();
        }

        activeHolders = new ItemHolder[totalItemsToFind];
        activePlaceableItems = new PlaceableItem[totalItemsToFind];
        int i = 0;
        while(i < allTheMandatoryItemHolders.Length)
        {
            activeHolders[i] = allTheMandatoryItemHolders[i];
            activePlaceableItems[i] = allTheMandatoryPlaceableItems[i];
            i++;
        }

        int[] randomIndexes = Utils.GenerateRandomNumbers(totalItemsToFind- allTheMandatoryItemHolders.Length, 0, allTheOptionalItemHolders.Length);
        int j = 0;
        while(i < totalItemsToFind)
        {
            activeHolders[i] = allTheOptionalItemHolders[randomIndexes[j]];
            activePlaceableItems[i] = allTheOptionalPlaceableItems[randomIndexes[j]];
            i++;
            j++;
        }

        foreach (var holder in activeHolders)
        {
            holder.Activate();
            holder.OnMouseOver.AddListener(OnHolderMouseOver);
            holder.OnMouseOut.AddListener(OnHolderMouseOut);
            holder.OnStatusChange.AddListener(HolderStatusChanged);
        }

        itemSpawn = itemSpawn.ToList().OrderBy(x => UnityEngine.Random.value).ToArray();
        for (int k = 0; k < itemSpawn.Length; k++)
        {
            activePlaceableItems[k].SetStartingPosition(itemSpawn[k].position);
        }

        foreach (var infoItem in onlyInfoObjects)
        {
            infoItem.OnClick.AddListener(OnlyInfoObjectClick);
        }

        maxAchievablePoints = activeHolders.Length;
        totalPoints = maxAchievablePoints;

        hint.Initialize();
        hint.ShowHint.AddListener(TimeToShowHint);
    }

    private void OnlyInfoObjectClick(string objectId)
    {
        ShowItemInfoPopup(objectId, true);
    }

    private void ShowItemInfoPopup(string objectId, bool makeClickSound = false)
    {
        if (!mainObjectPlacedPopup.IsShowing && !pausePopUp.IsShowing)
        {
            HideClueTooltip();
            mainObjectPlacedPopup.Show(ObjectNames.GetObjectFullDataById(objectId));
            mainObjectPlacedPopup.OnCloseButtonClickEvent.AddListener(MainObjectPopupClose);

            if (makeClickSound)
                MySoundManager.PlaySfxSound("Sound/PlaceObject/SFXPutCoin");
        }
    }

    private void TimeToShowHint()
    {
        foreach (var holder in activeHolders)
        {
            if(!holder.IsUsed)
            {
                hint.ShowWorldHintInPositionFrom3D(holder.transform);

                foreach (var placeableItem in activePlaceableItems)
                {
                    if(placeableItem.Id == holder.Id)
                        hint.ShowListHintInPositionFrom3D(placeableItem.transform);
                }
            }
        }
    }

    private string showingTooltipForId = "";
    private float showingTooltipSince = 0;

    private void OnHolderMouseOver(string objectId, Vector3 objectWorldPosition)
    {
        if (SpecialObjects.Contains(objectId))
        {
            if (!mainObjectPlacedPopup.IsShowing && !pausePopUp.IsShowing)
            {
                CancelCloseTooltipCorroutine();

                showingTooltipForId = objectId;
                showingTooltipSince = Time.time;
                coinInfoTooltip.Show(ObjectNames.GetObjectFullDataById(objectId).Clue, Utils.WorldToUIPoint(objectWorldPosition, gameCamera));
            }
        }
    }

    private void OnHolderMouseOut(string objectId, Vector3 objectWorldPosition)
    {
        CancelCloseTooltipCorroutine();

        if (SpecialObjects.Contains(objectId))
        {
            if (showingTooltipForId != objectId || ((Time.time - showingTooltipSince) > 0.25f))
            {
                HideClueTooltip();                
            }
            else
            {
                closeTooltipCorroutine = StartCoroutine(CheckIfCloseTooltip());
            }
        }
    }

    private Coroutine closeTooltipCorroutine;
    private void CancelCloseTooltipCorroutine()
    {
        if (closeTooltipCorroutine != null)
            StopCoroutine(closeTooltipCorroutine);

        closeTooltipCorroutine = null;
    }

    private IEnumerator CheckIfCloseTooltip()
    {
        yield return new WaitForSeconds(0.5f);
        HideClueTooltip();
    }

    private void HideClueTooltip()
    {
        showingTooltipForId = "";
        showingTooltipSince = 0;
        coinInfoTooltip.Hide();

        CancelCloseTooltipCorroutine();
    }

    private void HolderStatusChanged(string objectId, bool isRightPlacement)
    {
        if (!isRightPlacement)
        {
            totalPoints--;
            failedAttemptsInARow++;
            if (failedAttemptsInARow >= failedAttemptsUntilPunish)
                StartCoroutine(ShowPunishment());
        }
        else
        {
            failedAttemptsInARow = 0;

            if(SpecialObjects.Contains(objectId))
            {
                isPaused = true;

                ShowItemInfoPopup(objectId);
            }
            else
            {
                CheckIfWin();
            }            
        }
    }

    private void CheckIfWin()
    {
        bool allOk = true;
        foreach (var holder in activeHolders)
        {
            allOk = allOk && holder.IsRight;
        }

        if (allOk)
        {
            StartCoroutine(WinGame());
        }
    }

    private void MainObjectPopupClose()
    {
        isPaused = false;

        mainObjectPlacedPopup.OnCloseButtonClickEvent.RemoveAllListeners();
        mainObjectPlacedPopup.Hide();

        CheckIfWin();
    }

    private IEnumerator ShowPunishment()
    {
        punishBlocker.SetActive(true);
        yield return new WaitForSeconds(timeOfPunishment);
        failedAttemptsInARow = 0;
        punishBlocker.SetActive(false);
    }

    private IEnumerator WinGame()
    {
        hint.Stop();

        bool isInMainStage = btnZoomIn.activeSelf;

        //Deshabilito todos los colliders para que ya no muevan nada mas.
        foreach (var holder in activeHolders)
        {
            holder.IsInEnabledStage = false;
        }
        btnZoomIn.SetActive(false);
        btnZoomOut.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        if(!isInMainStage) //Esta en stage2
        {
            stagesAnimator.Play("secondToMainTransition");
            yield return new WaitForSeconds(.6f);

            //Vuelvo a deshabilitar todos los colliders para que ya no muevan nada mas, porque la animación lo reactiva.
            foreach (var holder in activeHolders)
            {
                holder.IsInEnabledStage = false;
            }
        }

        MySoundManager.PlaySfxSound("Sound/PlaceObject/SFXComplete");
        yield return new WaitForSeconds(0.7f);
        ShowGameOverPopup(true, totalPoints, maxAchievablePoints);
    }

#if DEBUG
    private void Update()
    {
        debugText.text = "maxAchievablePoints: " + maxAchievablePoints + "\n" +
                         "totalPoints: " + totalPoints;
    }
#endif

    public void BtnGanarClick()
    {
        ShowGameOverPopup(true, maxAchievablePoints, maxAchievablePoints);
    }

    public void BtnPerderClick()
    {
        ShowGameOverPopup(false, 0, maxAchievablePoints);
    }

    public void ZoomIn()
    {
        btnZoomIn.SetActive(false);
        btnZoomOut.SetActive(true);

        stagesAnimator.Play("mainToSecondTransition");
    }

    public void ZoomOut()
    {
        btnZoomIn.SetActive(true);
        btnZoomOut.SetActive(false);

        stagesAnimator.Play("secondToMainTransition");
    }

    public override void Pause()
    {
        base.Pause();
        hint.Pause();
    }

    protected override void Resume()
    {
        base.Resume();
        hint.Resume();
    }

    protected override void ContinueToNextLevel()
    {
        SceneManager.LoadScene("FinishScreen");
    }

    protected override void PlayAgain()
    {
        SceneManager.LoadScene("Game03_PlaceObj");
    }

    protected override void MoreInfo()
    {
        Application.OpenURL(Links.InfoLinkObjetosMuseo);
    }

    protected override string ResultText(int achievedStars)
    {
        switch (achievedStars)
        {
            case 0:
                return "El orden no es tu fuerte, ¡Gracias igual!";
            case 1:
                return "¡Bien!";
            case 2:
                return "¡Lo hiciste muy bien!";
            case 3:
                return "¡Genial!";
            default:
                return "";
        }
    }

    protected override int LvlNumber()
    {
        return 3;
    }
}
