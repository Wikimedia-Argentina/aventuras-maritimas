using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectTutorial : TutorialPopup
{
    protected override void ReproduceTutorialAnim()
    {
        MySoundManager.PlayVoiceSound("Sound/HiddenObject/VozHidden");
    }
}
