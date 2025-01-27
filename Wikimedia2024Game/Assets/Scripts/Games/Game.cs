using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviourWithContext
{
    [SerializeField] protected PausePopUp pausePopUp;
    [SerializeField] private GameOverPopup gameOverPopUp;
    [SerializeField] private TutorialPopup tutorialPopUp;
    public bool isPaused { get; protected set; }

    private void Start()
    {
        Context.Instance.Hello();

        isPaused = false;
        pausePopUp.Hide();
        gameOverPopUp.Hide();
        tutorialPopUp.Hide();

        ShowTutorial();

        StartGame();
    }

    protected virtual void ShowTutorial()
    {
        isPaused = true;
        tutorialPopUp.Show();
        tutorialPopUp.OnCloseButtonClickEvent.AddListener(CloseTutorial);
    }

    protected virtual void CloseTutorial()
    {
        isPaused = false;

        tutorialPopUp.Hide();
        tutorialPopUp.OnCloseButtonClickEvent.RemoveAllListeners();
    }

    protected virtual void StartGame()
    {
    }

    protected void ShowGameOverPopup(bool isWin, int pointsMade, int maxAchievablePoints)
    {
        int achievedStars = AchievedStars(pointsMade, maxAchievablePoints);

        MyPlayerStatus.SaveStarsByLevel(LvlNumber(), achievedStars);

        gameOverPopUp.Show(isWin, pointsMade, achievedStars, ResultText(achievedStars));
        gameOverPopUp.OnPlayAgainButtonClickEvent.AddListener(PlayAgain);
        gameOverPopUp.OnContinueButtonClickEvent.AddListener(ContinueToNextLevel);
        gameOverPopUp.OnCloseButtonClickEvent.AddListener(ExitGame);
        gameOverPopUp.OnMoreInfoButtonClickEvent.AddListener(MoreInfo);
    }

    protected virtual int LvlNumber()
    {
        throw new NotImplementedException();
    }

    protected virtual string ResultText(int achievedStars)
    {
        return "";
    }

    protected virtual int AchievedStars(int pointsMade, int maxAchievablePoints)
    {
        if(pointsMade >= maxAchievablePoints)
            return 3;

        if(pointsMade >= maxAchievablePoints * 0.6f)
            return 2;

        if (pointsMade >= maxAchievablePoints * 0.2f)
            return 1;

        return 0;
    }

    public virtual void Pause()
    {
        isPaused = true;
        MySoundManager.PauseAll();

        pausePopUp.Show();
        pausePopUp.OnCloseButtonClickEvent.AddListener(Resume);
        pausePopUp.OnBackToHomeClickEvent.AddListener(ExitGame);
    }

    protected virtual void Resume()
    {
        isPaused = false;
        MySoundManager.ResumeAll();

        pausePopUp.OnCloseButtonClickEvent.RemoveAllListeners();
        pausePopUp.OnBackToHomeClickEvent.RemoveAllListeners();
        pausePopUp.Hide();
    }

    protected virtual void ExitGame()
    {
        SceneManager.LoadScene("Home");
    }

    protected virtual void ContinueToNextLevel()
    {
        throw new NotImplementedException();
    }

    protected virtual void PlayAgain()
    {
        throw new NotImplementedException();
    }

    protected virtual void MoreInfo()
    {
        throw new NotImplementedException();
    }
}
