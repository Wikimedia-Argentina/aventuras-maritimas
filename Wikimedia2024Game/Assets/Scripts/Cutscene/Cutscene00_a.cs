using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene00_a : Cutscene
{
    protected override void StartCutscene()
    {
        base.StartCutscene();
        MyPlayerStatus.ResetStarsByLevel();

        //MySoundManager.PlayVoiceSound("Sound/Cutscenes/VozIntro");
    }

    protected override string NextScene()
    {
        MySoundManager.StopAllSounds();
        return "Cutscene00_b";
    }
}
