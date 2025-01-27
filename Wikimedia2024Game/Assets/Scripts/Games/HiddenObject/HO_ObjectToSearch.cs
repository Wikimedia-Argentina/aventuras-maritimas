using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class HO_ObjectToSearch : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField] private GameObject crossLine;
    [SerializeField] private Color notFoundColor;
    [SerializeField] private Color foundColor;

    public string Id { get; private set; }
    public int AmountToFind { get; private set; }
    public bool Done { get { return found >= AmountToFind; } }

    private int found = 0;

    private void Start()
    {
        label.color = notFoundColor;
        crossLine.SetActive(false);
    }

    public void Initialize(string objId, int amountToFind)
    {
        Id = objId;
        AmountToFind = amountToFind;
        found = 0;
        UpdateLabel();
    }

    public void CheckFound()
    {
        found++;
        UpdateLabel();

        crossLine.SetActive(Done);
        if(Done)
            label.color = foundColor;
    }

    private void UpdateLabel()
    {
        label.text = ObjectNames.GetObjectNameById(Id);

        if (AmountToFind != 1)
        {
            if (found == 0)
                label.text += " " + "x" + AmountToFind;
            else
                label.text += " " + found + "/" + AmountToFind;
        }
    }

    public IEnumerator HighLightForHint(float time)
    {
        var currentFontSize = label.fontSize;

        label.color = Color.white;
        label.fontSize = currentFontSize + 2;

        yield return new WaitForSeconds(time);

        label.color = Done ? foundColor : notFoundColor;
        label.fontSize = currentFontSize;
    }
}
