using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerTutorial : TutorialPopup
{
    protected override void ReproduceTutorialAnim()
    {
        MySoundManager.PlayVoiceSound("Sound/Runner/VozRunner");
    }
}
