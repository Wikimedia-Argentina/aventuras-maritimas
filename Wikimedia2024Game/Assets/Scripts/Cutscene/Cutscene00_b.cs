using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene00_b : Cutscene
{
    protected override string NextScene()
    {
        MySoundManager.StopAllSounds();
        return "Game01_Runner";
    }
}
