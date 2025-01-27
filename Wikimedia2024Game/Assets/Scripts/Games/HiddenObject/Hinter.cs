using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hinter : MonoBehaviourWithContext
{
    [SerializeField] private float timeforHintInSeconds = 45;
    [SerializeField] private Image timerView;
    [SerializeField] private Button buttonShowHint;
    [SerializeField] private Animator worldhintAnim;
    [SerializeField] private Animator listhintAnim;
    [SerializeField] private Camera worldCamera;

    public UnityEvent ShowHint;

    private float time = 0;
    private bool isPaused = false;

    public void Initialize()
    {
        Reset();
        StartCoroutine(CheckTimeForHint());
    }

    public void Stop()
    {
        StopAllCoroutines();
    }

    private IEnumerator CheckTimeForHint()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            while (isPaused) { yield return new WaitForEndOfFrame(); }
            time++;

            timerView.fillAmount = time / timeforHintInSeconds;

            if (time >= timeforHintInSeconds)
            {
                buttonShowHint.gameObject.SetActive(true);
                MySoundManager.PlaySfxSound("Sound/HiddenObject/SFXHint");

                while (time >= timeforHintInSeconds)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }

    public void ButtonShowHintClicked()
    {
        MySoundManager.PlaySfxSound("Sound/HiddenObject/SFXHint");
        ShowHint.Invoke();
        Reset();
    }

    private void Reset()
    {
        time = 0;
        timerView.fillAmount = 0;
        buttonShowHint.gameObject.SetActive(false);
    }

    public void ShowWorldHintInPositionFromUI(Transform transformToHint)
    {
        worldhintAnim.transform.position = transformToHint.position;
        worldhintAnim.Play("HintShow");
    }

    public void ShowWorldHintInPositionFrom3D(Transform transformToHint)
    {
        worldhintAnim.transform.position = Utils.WorldToUIPoint(transformToHint, worldCamera);
        worldhintAnim.Play("HintShow");
    }

    public void ShowListHintInPositionFromUI(Transform transformToHint)
    {
        listhintAnim.transform.position = transformToHint.position;
        listhintAnim.Play("HintShow");
    }

    public void ShowListHintInPositionFrom3D(Transform transformToHint)
    {
        listhintAnim.transform.position = Utils.WorldToUIPoint(transformToHint, worldCamera);
        listhintAnim.Play("HintShow");
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Resume()
    {
        isPaused = false;
    }
}
