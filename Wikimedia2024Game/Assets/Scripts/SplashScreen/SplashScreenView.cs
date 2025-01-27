using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenView : MonoBehaviourWithContext
{
    void Start()
    {
        Context.Instance.Hello();
    }

    public void StartMusic()
    {
        MySoundManager.PlayMusicLoop("Sound/MusicSplash");
    }

    public void Continue()
    {
        SceneManager.LoadScene("Home");
    }
}
