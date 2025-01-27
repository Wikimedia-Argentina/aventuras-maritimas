using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScreen : MonoBehaviourWithContext
{
    [SerializeField] private GameObject star1;
    [SerializeField] private GameObject star2;
    [SerializeField] private GameObject star3;

    [SerializeField] private TMP_Text resultTip;

    private void Awake()
    {
        Context.Instance.Hello();
        MySoundManager.PlayMusicLoop("Sound/MusicSplash");
    }

    private void Start()
    {
        int totalStars = 0;
        for (int i = 0; i < MyPlayerStatus.StarsByLevel.Length; i++)
        {
            totalStars += MyPlayerStatus.StarsByLevel[i];
        }
        totalStars = Mathf.RoundToInt(totalStars / MyPlayerStatus.StarsByLevel.Length);

        star1.SetActive(totalStars >= 1);
        star2.SetActive(totalStars >= 2);
        star3.SetActive(totalStars >= 3);

        resultTip.text = ResultText(totalStars);
    }

    private string ResultText(int achievedStars)
    {
        switch (achievedStars)
        {
            case 0:
                return "";
            case 1:
                return "¡Bien!";
            case 2:
                return "¡Muy buen juego!";
            case 3:
                return "¡Excelente!";
            default:
                return "";
        }
    }

    public void MoreInfo()
    {
        Application.OpenURL(Links.InfoLinkSalaMuseo);
    }

    public virtual void ExitGame()
    {
        SceneManager.LoadScene("Home");
    }

    public virtual void PlayAgain()
    {
        SceneManager.LoadScene("Cutscene00_a");
    }
}
