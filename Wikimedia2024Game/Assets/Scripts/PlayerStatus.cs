using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int[] StarsByLevel { get; private set; }

    private void Awake()
    {
        ResetStarsByLevel();
    }

    public void ResetStarsByLevel()
    {
        StarsByLevel = new int[] { 0, 0, 0 };
    }

    public void SaveStarsByLevel(int lvl, int stars)
    {
        if (StarsByLevel[lvl - 1] < stars)
            StarsByLevel[lvl - 1] = stars;
    }
}
