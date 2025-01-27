using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CLickforInfoObject : MonoBehaviour
{
    [SerializeField] private GameObject stroke;
    [SerializeField] private string Id;
    [SerializeField] private bool IsInEnabledStage;

    public UnityEvent<string> OnClick;

    private bool isMouseOver = false;

    private void OnMouseEnter()
    {
        if (IsInEnabledStage)
            isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }

    private void Update()
    {
        stroke.SetActive(isMouseOver);

        if(isMouseOver && Input.GetMouseButtonUp(0))
        {
            OnClick.Invoke(Id);
        }
    }
}
