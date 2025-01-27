using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviourWithContext
{
    private void Start()
    {
        Context.Instance.Hello();
        //MySoundManager.StopMusic();

        StartCutscene();
    }

    protected virtual void StartCutscene()
    {

    }

    public void Continue()
    {
        SceneManager.LoadScene(NextScene());
    }

    protected virtual string NextScene()
    {
        throw new NotImplementedException();
    }
}
