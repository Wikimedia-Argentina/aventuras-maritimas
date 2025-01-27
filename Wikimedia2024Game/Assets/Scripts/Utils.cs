using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utils
{
    public static Vector3 UIToWorldPoint(Transform uiPoint, Camera camera)
    {
        return UIToWorldPoint(uiPoint.position, camera);
    }

    public static Vector3 UIToWorldPoint(Vector3 uiPos, Camera camera)
    {
        var pos = camera.ScreenToWorldPoint(uiPos);
        pos.z = 0;
        return pos;
    }

    public static Vector3 WorldToUIPoint(Transform worldPoint, Camera camera)
    {
        return WorldToUIPoint(worldPoint.position, camera);
    }

    public static Vector3 WorldToUIPoint(Vector3 worldPos, Camera camera)
    {
        var pos = camera.WorldToScreenPoint(worldPos);
        pos.z = 0;
        return pos;
    }

    public static int[] GenerateRandomNumbers(int amount, int from, int to)
    {
        var theList = new List<int>();
        for (int i = from; i < to; i++)
        {
            theList.Add(i);
        }
        theList = theList.OrderBy(x => UnityEngine.Random.value).ToList();

        var randomNumbers = new int[amount];
        for (int i = 0; i < randomNumbers.Length; i++)
        {
            randomNumbers[i] = theList[i];
        }
        return randomNumbers;
    }
}
