using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene02 : Cutscene
{
    protected override string NextScene()
    {
        return "Game03_PlaceObj";
    }
}
