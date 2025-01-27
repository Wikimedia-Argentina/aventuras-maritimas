using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene01_a : Cutscene
{
    protected override string NextScene()
    {
        return "Cutscene01_b";
    }
}
