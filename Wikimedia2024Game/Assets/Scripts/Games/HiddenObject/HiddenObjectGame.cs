using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiddenObjectGame : Game
{
    [SerializeField] private TMP_Text debugText;

    [SerializeField] private Animator buzoAnim;

    [SerializeField] private MainObjectFoundPopupUp mainObjectFoundPopup;

    [SerializeField] private HO_Object[] allTheOtherObjects;
    [SerializeField] private HO_Object[] justOneOfTheseObjectsObjects;
    [SerializeField] private HO_Object[] justOneOfTheseObjectsObjectsAndTheOthersGone;
    [SerializeField] private HO_Object[] mandatoryObjects;
    [SerializeField] private HO_Object[] allTheDummyObjects;
    [SerializeField] private HO_ObjectToSearch[] objectsToSearch;
    [SerializeField] private Slider pointsBar;
    [SerializeField] private TMP_Text timer;
    [SerializeField] private Hinter hint;
    [SerializeField] private ComboEffect comboEffect;
    [SerializeField] private Animator comboAnim;
    [SerializeField] private Animator superComboAnim;

    [SerializeField] private GameObject punishBlocker;
    [SerializeField] private float timeOfPunishment = 3;

    private float timeSinceLastRightClick = 1000;
    private float timeSinceLastWrongClick = 1000;
    private int rightInARow = 0;
    private int wrongInARow = 0;
    [SerializeField] private float DoubleBonusTime = 4;
    [SerializeField] private float TripleBonusTime = 2;
    [SerializeField] private float SmashingAmount = 5;
    [SerializeField] private float SmashingTime = 8;

    private int idealTimeToWin;
    private int timeElapsed;
    private int countUnitsToFind;
    private int maxAchievablePoints;
    private int totalPoints;

    protected override void StartGame()
    {
        MySoundManager.PlayMusicLoop("Sound/HiddenObject/MusicHidden");

        mainObjectFoundPopup.Hide();

        debugText.text = "";

        punishBlocker.SetActive(false);

        foreach (var obj in justOneOfTheseObjectsObjects)
        {
            obj.OnObjUnitClicked.AddListener(ObjectClicked);
        }
        foreach (var obj in justOneOfTheseObjectsObjectsAndTheOthersGone)
        {
            obj.OnObjUnitClicked.AddListener(ObjectClicked);
        }
        foreach (var obj in mandatoryObjects)
        {
            obj.OnObjUnitClicked.AddListener(ObjectClicked);
        }
        foreach (var obj in allTheOtherObjects)
        {
            obj.OnObjUnitClicked.AddListener(ObjectClicked);
        }
        foreach (var obj in allTheDummyObjects)
        {
            obj.OnObjUnitClicked.AddListener(ObjectClicked);
        }

        allTheOtherObjects = allTheOtherObjects.ToList().OrderBy(x => UnityEngine.Random.value).ToArray();
        mandatoryObjects = mandatoryObjects.ToList().OrderBy(x => UnityEngine.Random.value).ToArray();
        justOneOfTheseObjectsObjectsAndTheOthersGone = justOneOfTheseObjectsObjectsAndTheOthersGone.ToList().OrderBy(x => UnityEngine.Random.value).ToArray();

        countUnitsToFind = 0;

        int i = 0;
        while (i < objectsToSearch.Length)
        {
            if(i < mandatoryObjects.Length)
            {
                objectsToSearch[i].Initialize(mandatoryObjects[i].Id, mandatoryObjects[i].AmountToFind);
                countUnitsToFind += mandatoryObjects[i].AmountToFind;
            }
            else if(i == mandatoryObjects.Length)
            {
                int randomObj = UnityEngine.Random.Range(0, justOneOfTheseObjectsObjects.Length);
                objectsToSearch[i].Initialize(justOneOfTheseObjectsObjects[randomObj].Id, justOneOfTheseObjectsObjects[randomObj].AmountToFind);
                countUnitsToFind += justOneOfTheseObjectsObjects[randomObj].AmountToFind;
            }
            else if (i == mandatoryObjects.Length+1)
            {
                objectsToSearch[i].Initialize(justOneOfTheseObjectsObjectsAndTheOthersGone[0].Id, justOneOfTheseObjectsObjectsAndTheOthersGone[0].AmountToFind);
                countUnitsToFind += justOneOfTheseObjectsObjectsAndTheOthersGone[0].AmountToFind;

                for (int j = 1; j < justOneOfTheseObjectsObjectsAndTheOthersGone.Length; j++)
                {
                    justOneOfTheseObjectsObjectsAndTheOthersGone[j].RemoveFromGame();
                }
            }
            else
            {
                objectsToSearch[i].Initialize(allTheOtherObjects[i].Id, allTheOtherObjects[i].AmountToFind);
                countUnitsToFind += allTheOtherObjects[i].AmountToFind;
            }

            i++;
        }

        StartCoroutine(FixTextAlignment());

        idealTimeToWin = countUnitsToFind * 10; //10 segundos por objeto
        maxAchievablePoints = countUnitsToFind * 10 + idealTimeToWin;
        totalPoints = maxAchievablePoints;

        pointsBar.maxValue = maxAchievablePoints;
        pointsBar.minValue = 0;
        pointsBar.value = maxAchievablePoints;

        hint.Initialize();
        hint.ShowHint.AddListener(TimeToShowHint);

        StartCoroutine(CheckTime());
    }

    [SerializeField] private HorizontalLayoutGroup[] layouts;
    private IEnumerator FixTextAlignment()
    {
        yield return new WaitForEndOfFrame();
        foreach (var item in layouts)
        {
            item.enabled = false;
        }
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        foreach (var item in layouts)
        {
            item.enabled = true;
        }
    }

    private void ShowHintInListItem(HO_ObjectToSearch listItem)
    {
        StartCoroutine(listItem.HighLightForHint(5f));
        //hint.ShowListHintInPositionFromUI(listItem.transform);
    }

    private void TimeToShowHint()
    {
        for (int i = 0; i < objectsToSearch.Length; i++)
        {
            if (!objectsToSearch[i].Done)
            {
                foreach (var obj in mandatoryObjects)
                {
                    if (obj.Id == objectsToSearch[i].Id)
                    {
                        hint.ShowWorldHintInPositionFromUI(obj.PositionOfANotFoundUnit);
                        ShowHintInListItem(objectsToSearch[i]);                        
                    }
                }
                foreach (var obj in justOneOfTheseObjectsObjects)
                {
                    if (obj.Id == objectsToSearch[i].Id)
                    {
                        hint.ShowWorldHintInPositionFromUI(obj.PositionOfANotFoundUnit);
                        ShowHintInListItem(objectsToSearch[i]);
                    }
                }
                if (justOneOfTheseObjectsObjectsAndTheOthersGone[0].Id == objectsToSearch[i].Id)
                {
                    hint.ShowWorldHintInPositionFromUI(justOneOfTheseObjectsObjectsAndTheOthersGone[0].PositionOfANotFoundUnit);
                    ShowHintInListItem(objectsToSearch[i]);
                }
                foreach (var obj in allTheOtherObjects)
                {
                    if(obj.Id == objectsToSearch[i].Id)
                    {
                        hint.ShowWorldHintInPositionFromUI(obj.PositionOfANotFoundUnit);
                        ShowHintInListItem(objectsToSearch[i]);
                    }
                }
                
                return;
            }
        }
    }

    private IEnumerator CheckTime()
    {
        timeElapsed = 0;
        while(countUnitsToFind > 0)
        {
            var time = new TimeSpan(0, 0, 0, timeElapsed);
            timer.text = time.Minutes.ToString("00") +":"+ time.Seconds.ToString("00");
            yield return new WaitForSeconds(1);
            while (isPaused) { yield return new WaitForEndOfFrame(); }
            timeSinceLastRightClick++;
            timeSinceLastWrongClick++;
            timeElapsed++;

            if (timeElapsed > idealTimeToWin) //Una vez que se pasa del idealTimeToWIn, descuento 1 punto por segundo
            {
                totalPoints--;
                UpdatePointsUI();
            }
        }
    }

    private void UpdatePointsUI()
    {
        pointsBar.value = Math.Max(totalPoints, 2); //No la muestro nunca vacía.
    }

#if DEBUG
    private void Update()
    {
        debugText.text = "idealTimeToWin: " + idealTimeToWin + "\n" +
                         "elapsedTime: " + timeElapsed + "\n" +
                         "objectsToFind: " + countUnitsToFind + "\n" +
                         "maxAchievablePoints: " + maxAchievablePoints + "\n" +
                         "totalPoints: " + totalPoints + "\n" +
                         "rightInARow: " + rightInARow + "\n" +
                         "wrongInARow: " + wrongInARow + "\n" +
                         "timeSinceLastRightClick: " + timeSinceLastRightClick + "\n" +
                         "timeSinceLastWrongClick: " + timeSinceLastWrongClick;
    }
#endif

    private void ObjectClicked(HO_Object obj, HO_ObjectUnit unit)
    {
        bool gotItRight = false;
        for (int i = 0; i < objectsToSearch.Length; i++)
        {
            if(objectsToSearch[i].Id == obj.Id)
            {
                gotItRight = true;
                objectsToSearch[i].CheckFound();
            }
        }

        if (gotItRight)
        {
            buzoAnim.Play("FESTEJO");

            timeSinceLastWrongClick = 0;
            wrongInARow = 0;

            if (rightInARow > 1 && timeSinceLastRightClick <= TripleBonusTime)
            {
                rightInARow++;
            }
            else if (rightInARow == 1 && timeSinceLastRightClick <= DoubleBonusTime)
            {
                rightInARow++;
            }
            else
            {
                rightInARow = 1;
            }
            timeSinceLastRightClick = 0;

            countUnitsToFind--;
            if(rightInARow > 2)
            {
                comboEffect.PlaySuperCombo();
                //superComboAnim.Play("superComboShow");
                totalPoints += 15;
            }
            else if (rightInARow > 1)
            {
                comboEffect.PlayCombo();
                //comboAnim.Play("comboShow");
                totalPoints += 12;
            }
            else
            {
                totalPoints += 10;
            }

            obj.MarkUnitAsFound(unit);

            StartCoroutine(CheckIfMainObject(obj.Id));

            StartCoroutine(CheckIfWin());
        }
        else
        {
            unit.AnimateError();

            rightInARow = 0;
            timeSinceLastRightClick = 0;

            if (timeSinceLastWrongClick <= SmashingTime)
                wrongInARow++;
            else
            {
                timeSinceLastWrongClick = 0;
                wrongInARow = 1;
            }

            if (wrongInARow >= SmashingAmount)
            {
                wrongInARow = 0;
                timeSinceLastWrongClick = 0;
                StartCoroutine(ShowPunishment());
            }

            totalPoints -= 10;
        }

        UpdatePointsUI();
    }

    private IEnumerator CheckIfMainObject(string id)
    {
        if (id == "monedaHechizada")
        {
            yield return new WaitForSeconds(1);
            MainObjectFound();
        }
    }

    private IEnumerator ShowPunishment()
    {
        punishBlocker.SetActive(true);
        yield return new WaitForSeconds(timeOfPunishment);
        punishBlocker.SetActive(false);
    }

    private IEnumerator CheckIfWin()
    {
        if (countUnitsToFind == 0)
        {
            yield return new WaitForSeconds(3);
            hint.Stop();

            while (isPaused) { yield return new WaitForEndOfFrame(); }

            ShowGameOverPopup(true, totalPoints, maxAchievablePoints);
        }
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

    public void BtnGanarClick()
    {
        ShowGameOverPopup(true, maxAchievablePoints, maxAchievablePoints);
    }

    public void BtnPerderClick()
    {
        ShowGameOverPopup(false, 0, maxAchievablePoints);
    }

    protected override void ContinueToNextLevel()
    {
        SceneManager.LoadScene("Cutscene02");
    }

    protected override void PlayAgain()
    {
        SceneManager.LoadScene("Game02_HiddenObj");
    }

    protected override void MoreInfo()
    {
        Application.OpenURL(Links.InfoLinkNaufragios);
    }

    private void MainObjectFound()
    {
        isPaused = true;

        mainObjectFoundPopup.Show(ObjectNames.GetObjectFullDataById("monedaHechizada"));
        mainObjectFoundPopup.OnCloseButtonClickEvent.AddListener(MainObjectPopupClose);
    }

    private void MainObjectPopupClose()
    {
        isPaused = false;

        mainObjectFoundPopup.OnCloseButtonClickEvent.RemoveAllListeners();
        mainObjectFoundPopup.Hide();
    }

    protected override string ResultText(int achievedStars)
    {
        switch (achievedStars)
        {
            case 0:
                return "Es hora de visitar a un oculista...";
            case 1:
                return "¡Bien!";
            case 2:
                return "¡Lo hiciste muy bien!";
            case 3:
                return "¡Perfecto!";
            default:
                return "";
        }
    }

    protected override int LvlNumber()
    {
        return 2;
    }
}
