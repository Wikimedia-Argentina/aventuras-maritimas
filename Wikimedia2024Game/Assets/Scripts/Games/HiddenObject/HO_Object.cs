using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HO_Object : MonoBehaviour
{
    [SerializeField] public string Id;
    [SerializeField] public HO_ObjectUnit[] units;

    public UnityEvent<HO_Object, HO_ObjectUnit> OnObjUnitClicked;

    [SerializeField] public GameObject[] UnitsToTurnOfIfRemovedFromGame;

    private int unitsFound = 0;

    public int AmountToFind { get { return units.Length; } }

    public Transform PositionOfANotFoundUnit { 
        get 
        {
            foreach (var unit in units)
            {
                if(!unit.UnitFound)
                {
                    return unit.transform;
                }
            }
            Debug.LogError("ERROR: Este objecto ya no tiene unidades sin encontrar");
            return null;
        }
    }

    private void Start()
    {
        unitsFound = 0;

        foreach (var unit in units)
        {
            unit.UnitFound = false;
            unit.OnClicked.AddListener(UnitClicked);
        }
    }

    private void UnitClicked(HO_ObjectUnit unit)
    {
        OnObjUnitClicked.Invoke(this, unit);
    }

    public void MarkUnitAsFound(HO_ObjectUnit unit)
    {
        unit.AnimateRight(Id == "monedaHechizada");
    }

    public void RemoveFromGame()
    {
        foreach (var unit in UnitsToTurnOfIfRemovedFromGame)
        {
            unit.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
