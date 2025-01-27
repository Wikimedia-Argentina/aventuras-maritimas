using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectTutorial : TutorialPopup
{
    protected override void ReproduceTutorialAnim()
    {
        MySoundManager.PlayVoiceSound("Sound/PlaceObject/VozPuzzle");
    }
}
